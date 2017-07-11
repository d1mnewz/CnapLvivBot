﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Configuration.ConfigurationManager;
using static System.Convert;

namespace CnapLvivBot.Data.Infrastructure.Repository
{
    public class MongoDbRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected List<T> List { get; set; }
        private MongoClient _client;


        public MongoDbRepository()
        {
            List = new List<T>();
            var credential = MongoCredential.CreateCredential(databaseName: AppSettings["DatabaseName"], username: AppSettings["MongoDbUsername"], password: AppSettings["MongoDbPassword"]);

            var mongoClientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host: AppSettings["MongoDbServerAddress"], port: ToInt32(AppSettings["MongoDbServerPort"])),
                Credentials = new List<MongoCredential> { credential }
            };
            _client = new MongoClient(settings: mongoClientSettings);

            var targetDb = _client.GetDatabase(AppSettings["DatabaseName"]);
            var collection = targetDb.GetCollection<T>(typeof(T).Name);
            List = collection.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();


        }



        public async Task<bool> AnyAsync(string id) => List.Exists(x => x.id.Equals(id));

        public async Task<T> GetAsync(string id)
        {
            return List.FirstOrDefault(x => x.id.Equals(id));
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return List;
        }

        public async Task<List<T>> GetAsync(IList<string> ids)
        {
            return List.Where(x => ids.Contains(x.id)).ToList();
        }


        #region //TODO:





        public async Task InsertAsync(T entity)
        {
            List.Add(entity);
            // add to db
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            List.AddRange(entities);
            // add to db
        }

        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();

        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}