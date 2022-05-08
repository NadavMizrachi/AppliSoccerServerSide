using AppliSoccerDatabasing.DBModels;
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

        private IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = _databaseConnection.GetMongoDatabase();
            return db.GetCollection<T>(collectionName);
        }

        public IMongoCollection<TeamDBModel> GetTeamsCollection()
        {
            return GetCollection<TeamDBModel>(_teamsCollection);
        }

        public IMongoCollection<TeamDBModel> GetUserCollection()
        {
            return GetCollection<TeamDBModel>(_usersCollection);
        }
    }
}
