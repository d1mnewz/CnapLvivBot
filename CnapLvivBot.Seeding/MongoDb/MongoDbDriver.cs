using System;
using System.Collections.Generic;
using MongoDB.Driver;
using static System.Configuration.ConfigurationManager;
using static System.Convert;

namespace CnapLvivBot.Seeding.MongoDb
{
    public class MongoDbDriver : IDriver
    {
        protected readonly MongoClient Client;
        /// <exception cref="NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
        public MongoDbDriver()
        {
            //Client = new MongoClient(connectionString: AppSettings["MongoDbEndpointUrl"]);
            var credential = MongoCredential.CreateCredential(databaseName: AppSettings["DatabaseName"], username: AppSettings["MongoDbUsername"], password: AppSettings["MongoDbPassword"]);

            var mongoClientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host: AppSettings["MongoDbServerAddress"], port: ToInt32(AppSettings["MongoDbServerPort"])),
                Credentials = new List<MongoCredential> { credential }
            };
            Client = new MongoClient(mongoClientSettings);
        }

        public void Run()
        {
            var res = Client.GetDatabase("cnap");
            res.CreateCollection("hello");
        }
    }
}
