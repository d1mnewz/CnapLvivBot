using System.Collections.Generic;
using MongoDB.Driver;
using static System.Configuration.ConfigurationManager;
using static System.Convert;
using static MongoDB.Driver.MongoCredential;

namespace CnapLvivBot.Seeding.MongoDb.Extensions
{
    public static class MongoDbClientExtensions
    {
        public static MongoClient GetClient(this MongoClient client)
        {
            var credential = CreateCredential(databaseName: AppSettings["DatabaseName"], username: AppSettings["MongoDbUsername"], password: AppSettings["MongoDbPassword"]);

            var mongoClientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host: AppSettings["MongoDbServerAddress"], port: ToInt32(AppSettings["MongoDbServerPort"])),
                Credentials = new List<MongoCredential> { credential }
            };
            client = new MongoClient(settings: mongoClientSettings);
            return client;
        }
    }
}
