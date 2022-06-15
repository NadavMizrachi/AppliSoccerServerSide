using AppliSoccerDatabasing;
using AppliSoccerObjects.Modeling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerEngine.Orders
{
    public class OrdersManager
    {
        private static readonly log4net.ILog _logger = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        private OrderValidator _orderValidator = new OrderValidator();
        public async Task<bool> CreateOrder(Order order)
        {
            _logger.Info("Trying to create order...");
            if (!_orderValidator.IsValid(order))
            {
                _logger.Info("Order is not valid. Will not be saved in database");
                return false;
            }
            try
            {
                _logger.Info("Order is valid. Trying to save in database...");
                String orderId = await _dataBaseAPI.InsertOrder(order);
                if (orderId == null)
                {
                    _logger.Info("Saving to database has failed.");
                    return false;
                }
                _logger.Info("Saving order has succeed");
                order.ID = orderId;
                List<OrderReceiving> orderReceivings = await CreateOrderReceivingList(order);
                
                _logger.Info("Trying to insert the OrderReceiving list to database");
                await _dataBaseAPI.InsertOrderReceivings(orderReceivings);
            }
            catch (Exception ex)
            {
                
                _logger.Error("Error occurred during trying to save order in database", ex);
                _logger.Info("Rolling back, Removing the order...");
                _dataBaseAPI.RemoveOrder(order);
                return false;
            }
            _logger.Info("Order creating was fully successfull!");
            return true;
        }

        public Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            _logger.Info("Trying get orders from database...");
            return _dataBaseAPI.GetOrders(receiverId, fromTime, endTime);
        }

        private async Task<List<OrderReceiving>> CreateOrderReceivingList(Order order)
        {
            _logger.Info("Trying to update orderPerReceiver");
            HashSet<String> receiversIds = new HashSet<String>();
            // Iterate through role that the order should go to and fetch all members under that role
            List<TeamMember> allTeamMembers = await _dataBaseAPI.GetTeamMembers(order.TeamId);
            foreach (var role in order.RolesReceivers)
            {
                List<TeamMember> membersOfRole = allTeamMembers.Where(member => IsPlayerWithRole(member, role)).ToList();
                membersOfRole.ForEach(member => receiversIds.Add(member.ID));
            }

            // Iterate Throguh order receivers and add to receivers
            foreach (var receiverId in order.MemberIdsReceivers)
            {
                receiversIds.Add(receiverId);
            }

            // For each member of recievers, create orderReceiving and update the DB
            List<OrderReceiving> orderReceivings = CreateOrderReceivings(receiversIds, order);
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug("Order Receiving list: " + JsonConvert.SerializeObject(orderReceivings));
            }
            return orderReceivings;
        }

        private List<OrderReceiving> CreateOrderReceivings(HashSet<string> receiversIds, Order order)
        {
            List<OrderReceiving> result = new List<OrderReceiving>();
            foreach (var recieverId in receiversIds)
            {
                result.Add( new OrderReceiving() { ReceiverId = recieverId, Order = order, WasRead = false } );
            }
            return result;
        }

        private bool IsPlayerWithRole(TeamMember member, Role role)
        {
            if(member.MemberType != MemberType.Player)
            {
                return false;
            }
            return (member.AdditionalInfo as PlayerAdditionalInfo).Role == role;
        }
    }
}
