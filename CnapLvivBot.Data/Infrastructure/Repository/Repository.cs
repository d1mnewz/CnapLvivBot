using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Linq;

namespace CnapLvivBot.Data.Infrastructure.Repository
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        protected List<T> List { get; set; }
        private DocumentClient _client;


        public Repository()
        {
            List = new List<T>();
            _client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            var db = (_client.ReadDatabaseFeedAsync()).Result.Single(d => d.Id == DatabaseName);
            var collection = (_client.ReadDocumentCollectionFeedAsync(db.CollectionsLink)).Result.Single(c => c.Id == typeof(T).Name);
            var docs = _client.CreateDocumentQuery(collection.DocumentsLink).ToList();
            foreach (var document in docs)
            {
                List.Add(JObject.Parse(document.ToString()).ToObject<T>());
            }
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return List;
        }

        public async Task<List<T>> GetAsync(IList<TId> ids)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }


        #region Credentials

        private readonly string EndpointUrl = ConfigurationManager.AppSettings["EndpointUrl"];
        private readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        private readonly string DatabaseName = "CNAP"; // move to app.config

        #endregion
    }
}
