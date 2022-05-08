using AppliSoccerDatabasing;
using AppliSoccerDatabasing.mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine
{
    public class Database
    {
        private static IDataBaseAPI _databaseAPI;

        public static IDataBaseAPI GetDatabase()
        {
            if(_databaseAPI == null)
            {
                _databaseAPI = new MongoDatabase();
            }
            return _databaseAPI;
        }
    }
}
