using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing
{
    public interface IDataBaseAPI
    {
        public void CreateDatabase();
        public void CreateTables();
        public Task<List<Team>> GetUnregistredTeamsTask();
        public Task<List<Team>> GetUnregistredTeamsTask(string country);
        public Task InsertTeamTask(Team team);
        public Task<bool> IsTeamExistTask(Team team);
        public Task<bool> IsRegisteredTeamTask(string teamID);
        public Task<User> GetUser(string username);
        public Task<bool> IsUsernameExistTask(string username);
        public Task InsertUserTask(User user);
        public Task<TeamMember> UpdateMember(string userId, TeamMember memberNewDetails);
        public Task MarkTeamAsRegisterTask(string teamId);
        public Task<List<TeamMember>> GetTeamMembers(string teamId);
        public Task<bool> RemoveUser(string iD);
        public Task<bool> IsExistCoach(string teamId);
        public Task<String> InsertOrder(Order order);
        public Task RemoveOrder(Order order);
        public Task InsertOrderReceivings(List<OrderReceiving> orderReceivings);
        public Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime);
    }
}
