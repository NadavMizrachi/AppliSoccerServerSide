﻿using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class MongoDatabase : IDataBaseAPI
    {
        private CollectionsAccess _collectionAccess;
        private TeamQueries _teamQueries;
        private UserQueries _userQueries;
        private OrderQueries _orderQueries;
        private EventQueries _eventQueries;
        private LeagueQueries _leagueQueries;
        
        public MongoDatabase()
        {
            _collectionAccess = new CollectionsAccess();
            _teamQueries = new TeamQueries(_collectionAccess.GetTeamsCollection());
            _userQueries = new UserQueries(_collectionAccess.GetUserCollection());
            _orderQueries = new OrderQueries(_collectionAccess.GetOrdersCollection(), _collectionAccess.GetOrderReceivingCollection());
            _eventQueries = new EventQueries(_collectionAccess.GetEventsCollection());
            _leagueQueries = new LeagueQueries(_collectionAccess.GetLeaguesVollection());
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
            return _orderQueries.RemoveOrder(order);
        }

        public Task InsertOrderReceivings(List<OrderReceiving> orderReceivings)
        {
            return _orderQueries.InsertOrderReceiving(orderReceivings);
        }

        public Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            return _orderQueries.GetOrders(receiverId, fromTime, endTime);
        }

        public Task<List<OrderReceiving>> FetchOrdersMetadata(DateTime upperBoundDate, int ordersQuantity, string receiverId)
        {
            return _orderQueries.FetchOrdersMetadata(upperBoundDate, ordersQuantity, receiverId);
        }

        public Task<List<OrderReceiving>> PullNewOrdersMetadata(DateTime lowerBoundDate, int ordersQuantity, string receiverId)
        {
            return _orderQueries.PullNewOrdersMetadata(lowerBoundDate, ordersQuantity, receiverId);
        }

        public Task<Order> GetOrder(string orderId, string askerId)
        {
            return _orderQueries.GetOrder(orderId, askerId);
        }

        public async Task<TeamMember> GetTeamMember(string memberId)
        {
            User user = await _userQueries.GetTeamMemberOfUser(memberId);
            if (user == null)
                return null;
            return user.TeamMember;
        }

        public Task MarkOrderAsRead(string orderId, string askerId)
        {
            return Task.Run(() => _orderQueries.MarkOrderAsRead(orderId, askerId));
        }

        public Task<List<Order>> GetOrders(DateTime upperBoundDate, int ordersQuantity, string senderId)
        {
            return Task.Run( () => _orderQueries.GetOrders(upperBoundDate, ordersQuantity, senderId));
        }

        public Task<List<Order>> PullNewOrdersMetadataForSender(DateTime lowerBoundDate, int ordersQuantity, string senderId)
        {
            return Task.Run(() => _orderQueries.GetOrdersFromDateUp(lowerBoundDate, ordersQuantity, senderId));
        }

        public Task<Order> GetOrder(string orderId)
        {
            return Task.Run( () => _orderQueries.GetOrder(orderId) );
        }

        public Task<bool> IsExistOverlappingEvent(EventDetails eventDetails)
        {
            DateTime start = eventDetails.StartTime;
            DateTime end = eventDetails.EndTime;
            List<string> participantsIds = eventDetails.ParticipantsIds;

            return _eventQueries.isExistEventForParticipantsBetweenDates(start, end, participantsIds);
        }

        public Task InsertEvent(EventDetails eventDetails)
        {
            return _eventQueries.InsertEvent(eventDetails);
        }

        public Task<List<EventDetails>> GetEvents(DateTime lowerBoundDate, DateTime upBoundDate, string askerId)
        {
            return _eventQueries.GetEvents(lowerBoundDate, upBoundDate, askerId);
        }

        public Task UpdateEvent(EventDetails edittedEvent)
        {
            return _eventQueries.UpdateEvent(edittedEvent);
        }

        public Task<List<Team>> GetRegisteredTeams()
        {
            return _teamQueries.GetRegistredTeamsTask();
        }

        public Task UpdateTeam(Team team)
        {
            return _teamQueries.UpdateTeam(team);
        }

        /* Update only details - NO TABLES RANKS . TABLES RANK WILL BE UPDATED IN SEPERATE ROUTINE -  */
        public Task UpdateLeaguesDetails(List<League> leagues)
        {
            return _leagueQueries.UpdateLeaguesDetails(leagues);
        }

        public Task UpdateTableRanks(string leagueId, LeagueTable leagueTable)
        {
            return _leagueQueries.UpdateTableRanks(leagueId, leagueTable);
        }

        public Task<League> GetMainLeague(string teamId)
        {
            Team team = _teamQueries.GetTeam(teamId);
            return _leagueQueries.GetMainLeague(team.ExtMainLeagueId);
        }

        public Team GetTeam(string teamId)
        {
            return _teamQueries.GetTeam(teamId);
        }

        public Team GetTeamByExtId(string teamExtId)
        {
            return _teamQueries.GetTeamByExtId(teamExtId);
        }
    }
}
