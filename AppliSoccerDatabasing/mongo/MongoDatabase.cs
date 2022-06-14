using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class MongoDatabase : IDataBaseAPI
    {
        private CollectionsAccess _collectionAccess;
        private TeamQueries _teamQueries;
        private UserQueries _userQueries;
        private OrderQueries _orderQueries;
        
        public MongoDatabase()
        {
            _collectionAccess = new CollectionsAccess();
            _teamQueries = new TeamQueries(_collectionAccess.GetTeamsCollection());
            _userQueries = new UserQueries(_collectionAccess.GetUserCollection());
        }
        public void CreateDatabase()
        {
            // Database will be created when accessing it
        }

        public void CreateTables()
        {
            // Collections will be created when accessing it
        }

        public Task<List<TeamMember>> GetTeamMembers(string teamId)
        {
            return _userQueries.GetTeamMembers(teamId);
        }

        public Task<List<Team>> GetUnregistredTeamsTask()
        {
            return _teamQueries.GetUnregistredTeamsTask();
        }

        public Task<List<Team>> GetUnregistredTeamsTask(string country)
        {
            return _teamQueries.GetUnregistredTeamsTask(country);
        }

        public Task InsertTeamTask(Team team)
        {
            return _teamQueries.InsertTeamTask(team);
        }

        public Task InsertUserTask(User user)
        {
            return _userQueries.InsertUserTask(user);
        }


        public Task<bool> IsRegisteredTeamTask(string teamID)
        {
            return _teamQueries.IsRegisteredTeamTask(teamID);
        }


        public Task<bool> IsTeamExistTask(Team team)
        {
            return _teamQueries.IsTeamExistTask(team);
        }

        public Task<bool> IsUsernameExistTask(string username)
        {
            return _userQueries.IsUsernameExistTask(username);
        }

        public Task<User> GetUser(string username)
        {
            return _userQueries.GetTeamMemberOfUser(username);
        }

        public Task MarkTeamAsRegisterTask(string teamId)
        {
            return _teamQueries.MarkTeamAsRegisterTask(teamId);
        }

        public Task<TeamMember> UpdateMember(string userId, TeamMember memberNewDetails)
        {
            return _userQueries.UpdateMember(userId, memberNewDetails);
        }

        public Task<bool> RemoveUser(string userId)
        {
            return _userQueries.RemoveUser(userId);
        }

        public Task<bool> IsExistCoach(string teamId)
        {
            return _userQueries.IsCoachExit(teamId);
        }

        /// <summary>
        /// </summary>
        /// <param name="order"></param>
        /// <returns>ID of the order</returns>
        public Task<String> InsertOrder(Order order)
        {
            return _orderQueries.InsertOrder(order);
        }

        public Task RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrderReceivings(List<OrderReceiving> orderReceivings)
        {
            return _orderQueries.InsertOrderReceiving(orderReceivings);
        }

        public Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            return _orderQueries.GetOrders(receiverId, fromTime, endTime);
        }

    }
}
