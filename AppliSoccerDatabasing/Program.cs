using AppliSoccerDatabasing.DBModels;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //// Extract from configuration
            //string connectionString = "mongodb://root:root@localhost:27017";
            //string dbName = "test-db";
            //string collectionName = "test-collection";


            //var client = new MongoClient(connectionString);
            //var db = client.GetDatabase(dbName);
            //var collection = db.GetCollection<TestDBModel>(collectionName);

            //var testModel = new TestDBModel() { Id = "2", Age = 28, Name = "Nadav" };
            //await collection.InsertOneAsync(testModel);


            // Test configuration file


            Console.ReadLine();
        }
    }
}
