using System;
using MongoDB.Driver;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Seeding.MongoDb
{
    public class MongoDbDriver : IDriver
    {
        protected readonly MongoClient Client;
        /// <exception cref="NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
        public MongoDbDriver()
        {
            Client = new MongoClient(connectionString: AppSettings["MongoDbEndpointUrl"]);
        }

        public void Run()
        {
            var res = Client.GetDatabase("dima-test-db");
            res.CreateCollection("hello");
        }
    }
}
