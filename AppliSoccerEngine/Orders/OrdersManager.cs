using AppliSoccerDatabasing;
using AppliSoccerObjects.Modeling;
using AppliSoccerObjects.TeamMemberHelper;
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
                List<string> receiverIds = await ExtractReceiverIdsOfOrder(order);
                order.MemberIdsReceivers = receiverIds;
                String orderId = await _dataBaseAPI.InsertOrder(order);
                if (orderId == null)
                {
                    _logger.Info("Saving to database has failed.");
                    return false;
                }
                _logger.Info("Saving order has succeed");
                order.ID = orderId;
                List<OrderReceiving> orderReceivings = CreateOrderReceivings(receiverIds, order);
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

        public async Task<SentOrderWithReceiversInfo> GetOrder(string orderId)
        {
            Order order = await _dataBaseAPI.GetOrder(orderId);
            return new SentOrderWithReceiversInfo
            {
                Order = order,
                ReceiverInfos = await ExtractReceiversInfosOfOrder(order)
            };
        }

        public Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            _logger.Info("Trying get orders from database...");
            return _dataBaseAPI.GetOrders(receiverId, fromTime, endTime);
        }


        private async Task<List<string>> ExtractReceiverIdsOfOrder(Order order)
        {
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
            return receiversIds.ToList();
        }
        public async Task<List<OrderMetadata>> FetchOrdersMetadata(DateTime upperBoundDate, int ordersQuantity, string receiverId)
        {
            // Fetch from db
            List<OrderReceiving> orderReceivings = await _dataBaseAPI.FetchOrdersMetadata(upperBoundDate, ordersQuantity, receiverId);
            return orderReceivings
                .ConvertAll<OrderMetadata>(or => ConvertOrderReceivingToMetadata(or)) 
                .ToList<OrderMetadata>();
        }

        private OrderMetadata ConvertOrderReceivingToMetadata(OrderReceiving or)
        {
            // Get Sender Data
            var senderMember = _dataBaseAPI.GetUser(or.Order.SenderId).Result.TeamMember;
            // Get his full name
            var fullName = MemberFullNameGenerator.Generate(senderMember);
            return new OrderMetadata
            {
                OrderId = or.Order.ID,
                SenderName = fullName,
                SentDate = or.Order.SendingDate,
                Title = or.Order.Title,
                WasRead = or.WasRead
            };
        }

        private async Task <OrderMetadata> ConvertOrderToMetadata(Order or)
        {
            List<ReceiverInfo> recieverInfos = await ExtractReceiversInfosOfOrder(or);
            return new OrderMetadata
            {
                OrderId = or.ID,
                SentDate = or.SendingDate,
                Title = or.Title,
                ReceiversNames = recieverInfos.Select(info => info.Name).ToList(),
                RolesReceivers = or.RolesReceivers
            };
        }

        private async Task<List<ReceiverInfo>> ExtractReceiversInfosOfOrder(Order or)
        {
            List<string> receiversId = await ExtractReceiverIdsOfOrder(or);
            List<ReceiverInfo> receiverInfos = new List<ReceiverInfo>();
            foreach (var receiverId in receiversId)
            {
                var teamMember = _dataBaseAPI.GetTeamMember(receiverId).Result;
                var name = MemberFullNameGenerator.Generate(teamMember);
                var read = ( or.MembersIdsAlreadyRead.Contains(receiverId) );
                receiverInfos.Add(new ReceiverInfo { Name = name, Read = read});
            }
            return receiverInfos;
        }

        public async Task<List<OrderMetadata>> PullNewOrdersMetadata(DateTime lowerBoundDate, int ordersQuantity, string receiverId)
        {
            // Fetch from db
            List<OrderReceiving> orderReceivings = await _dataBaseAPI.PullNewOrdersMetadata(lowerBoundDate, ordersQuantity, receiverId);
            return orderReceivings
                .ConvertAll<OrderMetadata>(or => ConvertOrderReceivingToMetadata(or))
                .ToList<OrderMetadata>();
        }

        private List<OrderReceiving> CreateOrderReceivings(List<string> receiversIds, Order order)
        {
            List<OrderReceiving> result = new List<OrderReceiving>();
            foreach (var recieverId in receiversIds)
            {
                result.Add( new OrderReceiving() { ReceiverId = recieverId, Order = order, WasRead = false } );
            }
            return result;
        }

        public async Task<OrderPayload> GetOrder(string orderId, string askerId)
        {
            Order order =  await _dataBaseAPI.GetOrder(orderId, askerId);
            if(order == null)
            {
                _logger.Warn($"Order of id: ${orderId} is null");
                return null;
            }
            MarkOrderAsRead(orderId, askerId);
            return await CreateOrderPayload(order);
        }

        private void MarkOrderAsRead(string orderId, string askerId)
        {
            _dataBaseAPI.MarkOrderAsRead(orderId, askerId);
        }

        private async  Task<OrderPayload> CreateOrderPayload(Order order)
        {
            List<string> receiversIds = await ExtractReceiverIdsOfOrder(order);
            List<string> receiversNames =
                receiversIds.ToList()
                .Select(id => _dataBaseAPI.GetUser(id).Result.TeamMember)
                .Where(member => member != null)
                .Select(member => MemberFullNameGenerator.Generate(member))
                .ToList();
            return new OrderPayload
            {
                Content = order.Content,
                GameID = order.GameId,
                ReceiversNames = receiversNames,
                SenderName = MemberFullNameGenerator.Generate(await _dataBaseAPI.GetTeamMember(order.SenderId)),
                SendingDate = order.SendingDate,
                Title = order.Title
            };
        }

        public async Task<List<OrderMetadata>> FetchOrdersMetadataForSender(DateTime upperBoundDate, int ordersQuantity, string senderId)
        {
            // Get orders from DB
            List<Order> orders = await _dataBaseAPI.GetOrders(upperBoundDate, ordersQuantity, senderId);
            // Extract metadata
            // output
            List<OrderMetadata> output = await ExtractOrderMetadataList(orders);
            return output;
        }

        public async Task<List<OrderMetadata>> PullNewOrdersMetadataForSender(DateTime lowerBoundDate, int ordersQuantity, string senderId)
        {
            // Fetch from db
            List<Order> orderList = await _dataBaseAPI.PullNewOrdersMetadataForSender(lowerBoundDate, ordersQuantity, senderId);
            return orderList
                .ConvertAll<OrderMetadata>(or =>  ConvertOrderToMetadata(or).Result)
                .ToList();
        }

        private async Task<List<OrderMetadata>> ExtractOrderMetadataList(List<Order> orders)
        {
            List<OrderMetadata> output = new List<OrderMetadata>();
            foreach (var order in orders)
            {
                output.Add(await ConvertOrderToMetadata(order));
            }
            return output;
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
