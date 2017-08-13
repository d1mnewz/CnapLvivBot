using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Linq;

namespace CnapLvivBot.Data.Infrastructure.Repository
{
    public class DocumentDbRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected List<T> List { get; set; }
        private DocumentClient _client;


        public DocumentDbRepository()
        {
            List = new List<T>();
            _client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            var db = _client.ReadDatabaseFeedAsync().Result.Single(d => d.Id == DatabaseName);
            var collection = _client.ReadDocumentCollectionFeedAsync(db.CollectionsLink).Result.Single(c => c.Id == typeof(T).Name);
            var docs = _client.CreateDocumentQuery(collection.DocumentsLink).ToList();
            foreach (var document in docs)
            {
                List.Add(JObject.Parse(document.ToString()).ToObject<T>());
            }
        }



        public void Dispose()
        {
            _client.Dispose();
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

        #region Credentials from Web.config

        private readonly string EndpointUrl =  ConfigurationManager.AppSettings["EndpointUrl"];
        private readonly string PrimaryKey =   ConfigurationManager.AppSettings["PrimaryKey"];
        private readonly string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];

        #endregion
    }
}
