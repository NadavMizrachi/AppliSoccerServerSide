using AppliSoccerDatabasing;
using AppliSoccerEngine.TeamMembers;
using AppliSoccerObjects.Modeling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppliSoccerEngine.Orders
{
    internal class OrderValidator
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        public OrderValidator()
        {
        }

        public bool IsValid(Order order)
        {

            // For now, the client side will be reponsible to to varify the order

            return true;


            //TeamMember sender = _dataBaseAPI.GetTeamMembers(order.SenderId);
            //if (_logger.IsDebugEnabled)
            //{
            //    _logger.Debug("Sender object is :" + JsonConvert.SerializeObject(sender).ToString());
            //}
            //if(sender.MemberType == MemberType.Admin || sender.MemberType == MemberType.Player)
            //{
            //    _logger.Info("Admin ot Player cannot send orders!");
            //    return Task.FromResult(false);
            //}
            //// Member is Staff
            //if (TeamMemberTypeRecognizer.IsCoach(sender))
            //{
            //    // Coach can send order to all the members
            //    _logger.Info("sender is a coach, all valid!");
            //    return Task.FromResult(true);
            //}
            //List<Role> managedRoles = ManagedRolesExtractor.Extract(sender);
        }
    }
}