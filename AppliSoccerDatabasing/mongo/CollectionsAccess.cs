﻿using AppliSoccerDatabasing.DBModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.mongo
{
    public class CollectionsAccess
    {
        private DatabaseConnection _databaseConnection = new DatabaseConnection();
        private const string _teamsCollection = "teams";
        private const string _usersCollection = "users";
        private const string _ordersCollection = "orders";
        private const string _orderReceivingCollection = "orderReceiving";
        private const string _eventsCollection = "events";
        private const string _leaguesCollection = "leagues";

        private IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = _databaseConnection.GetMongoDatabase();
            return db.GetCollection<T>(collectionName);
        }

        public IMongoCollection<TeamDBModel> GetTeamsCollection()
        {
            return GetCollection<TeamDBModel>(_teamsCollection);
        }

        public IMongoCollection<UserDBModel> GetUserCollection()
        {
            return GetCollection<UserDBModel>(_usersCollection);
        }

        internal IMongoCollection<EventDetailsDBModel> GetEventsCollection()
        {
            return GetCollection<EventDetailsDBModel>(_eventsCollection);
        }

        public IMongoCollection<OrderDBModel> GetOrdersCollection()
        {
            return GetCollection<OrderDBModel>(_ordersCollection);
        }

        public IMongoCollection<OrderReceivingDBModel> GetOrderReceivingCollection()
        {
            return GetCollection<OrderReceivingDBModel>(_orderReceivingCollection);
        }

        public IMongoCollection<LeagueDBModel> GetLeaguesVollection()
        {
            return GetCollection<LeagueDBModel>(_leaguesCollection);
        }
    }
}
