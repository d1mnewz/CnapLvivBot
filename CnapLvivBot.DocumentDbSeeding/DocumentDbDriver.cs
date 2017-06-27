using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CnapLvivBot.DocumentDbSeeding
{
    public class DocumentDbSeeder
    {
        private DocumentClient _client;
        private readonly string _databaseName = ConfigurationManager.AppSettings["DatabaseName"];

        public DocumentDbSeeder()
        {
            _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["EndpointUrl"]), ConfigurationManager.AppSettings["PrimaryKey"]);
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseName }).Wait();

            CreateCollectionsAsync().Wait();
            InitCollections().Wait();

        }

        private async Task CreateCollectionsAsync()
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Intent).Name });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Response).Name });


        }

        private async Task InitCollections()
        {
            await InitIntents();
            await InitResponses();


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

        private async Task InitIntents()
        {

        }

        private async Task InitResponses()
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

    }
}