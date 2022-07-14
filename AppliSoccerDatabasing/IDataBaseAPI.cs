using AppliSoccerObjects.ActionResults.EventsActions;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing
{
    public interface IDataBaseAPI
    {
        void CreateDatabase();
        void CreateTables();
        Task<List<Team>> GetUnregistredTeamsTask();
        Task<List<Team>> GetUnregistredTeamsTask(string country);
        Task InsertTeamTask(Team team);
        Task<bool> IsTeamExistTask(Team team);
        Task<bool> IsRegisteredTeamTask(string teamID);
        Task<User> GetUser(string username);
        Task<bool> IsUsernameExistTask(string username);
        Task InsertUserTask(User user);
        Task<TeamMember> UpdateMember(string userId, TeamMember memberNewDetails);
        Task MarkTeamAsRegisterTask(string teamId);
        Task<List<TeamMember>> GetTeamMembers(string teamId);
        Task<List<Team>> GetRegisteredTeams();
        Task<TeamMember> GetTeamMember(string memberId);
        Task<bool> RemoveUser(string iD);
        Task<bool> IsExistCoach(string teamId);
        Task<String> InsertOrder(Order order);
        Task RemoveOrder(Order order);
        Task InsertOrderReceivings(List<OrderReceiving> orderReceivings);
        Task<bool> IsExistOverlappingEvent(EventDetails eventDetails);
        Task<League> GetMainLeague(string teamId);
        Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime);
        Task UpdateTeam(Team team);
        Task InsertEvent(EventDetails eventDetails);
        Task<List<OrderReceiving>> FetchOrdersMetadata(DateTime upperBoundDate, int ordersQuantity, string receiverId);
        Task<List<OrderReceiving>> PullNewOrdersMetadata(DateTime lowerBoundDate, int ordersQuantity, string receiverId);
        Task<Order> GetOrder(string orderId, string askerId);
        Task MarkOrderAsRead(string orderId, string askerId);
        Task UpdateLeaguesDetails(List<League> leagues);
        Team GetTeam(string teamId);
        Task<List<EventDetails>> GetEvents(DateTime lowerBoundDate, DateTime upBoundDate, string askerId);
        Task<List<Order>> GetOrders(DateTime upperBoundDate, int ordersQuantity, string senderId);
        Task<List<Order>> PullNewOrdersMetadataForSender(DateTime lowerBoundDate, int ordersQuantity, string senderId);
        Task<Order> GetOrder(string orderId);
        Task UpdateEvent(EventDetails edittedEvent);
        Task UpdateTableRanks(string leagueId, LeagueTable leagueTable);
        Team GetTeamByExtId(string teamExtId);
    }
}
