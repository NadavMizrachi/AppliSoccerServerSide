using AppliSoccerDatabasing.DBModels;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.mongo
{
    public class DatabaseConnection
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string _connectionString;
        private const string _dbName = "appliSoccer-db";


        public DatabaseConnection()
        {
            InitConnectionString();
            
        }

        private void RegisterObjectsHierarchy()
        {
            BsonClassMap.RegisterClassMap<PlayerAdditionalInfoDBModel>();
            BsonClassMap.RegisterClassMap<StaffAdditionalInfoDBModel>();
        }

        private void InitConnectionString()
        {
            _connectionString = MyConfiguration.GetStringFromAppSetting("connectionString");
            if(_connectionString.Length == 0)
            {
                _logger.Warn("Connection string from configuration file is zero length. Creating default string");
                _connectionString = "mongodb://root:root@localhost:27017";
            }
        }

        /// <summary>
        /// Returns Mongo Database object. If the database is not exist, it will create it.
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase GetMongoDatabase()
        {
            var client = new MongoClient(_connectionString);
            //RegisterObjectsHierarchy();
            return client.GetDatabase(_dbName);
        }
    }
}
