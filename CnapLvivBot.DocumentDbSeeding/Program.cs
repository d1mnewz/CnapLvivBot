using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CnapLvivBot.DocumentDbSeeding
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentDbDriver ddd = new DocumentDbDriver();

        }
    }

    public class DocumentDbDriver
    {
        private DocumentClient _client;
        private readonly string _databaseName = ConfigurationManager.AppSettings["DatabaseName"];

        public DocumentDbDriver()
        {
            _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["EndpointUrl"]), ConfigurationManager.AppSettings["PrimaryKey"]);
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseName }).Wait();

            CreateCollectionsAsync().Wait();
            InitCollections().Wait();
            var db = (_client.ReadDatabaseFeedAsync()).Result.Single(d => d.Id == _databaseName);

        }

        private async Task CreateCollectionsAsync()
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Intent).Name });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Response).Name });


        }

        private async Task InitCollections()
        {

            await CreateResponseDocumentIfNotExists(
                _databaseName,
                typeof(Response).Name,
                new Response
                {
                    Id = "RandomResponse",
                    Content = "Hello from CosmosDB",
                    Intents = new[]
                {
                    new Intent { Content = "intent1" },
                    new Intent { Content = "intent2" }
                }
                }
                );

        }
        private async Task CreateResponseDocumentIfNotExists(string databaseName, string collectionName, Response response)
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, response.Id));

            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), response);

                }
                else
                {
                    throw;
                }
            }
        }

    }
}
