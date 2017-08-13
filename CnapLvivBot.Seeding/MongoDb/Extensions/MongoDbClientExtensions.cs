using System;
using System.Collections.Generic;
using MongoDB.Driver;
using static System.Configuration.ConfigurationManager;
using static System.Convert;
using static MongoDB.Driver.MongoCredential;

namespace CnapLvivBot.Seeding.MongoDb.Extensions
{
    public static class MongoDbClientExtensions
    {
        /// <exception cref="OverflowException">
        ///         <paramref>
        ///             <name>value</name>
        ///         </paramref>
        ///     represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
        /// <exception cref="NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
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
