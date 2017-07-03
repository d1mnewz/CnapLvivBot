using System;
using System.Configuration;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.DocumentDbSeeding.Extensions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using static System.Console;
using static CnapLvivBot.DocumentDbSeeding.SeedValues.PreMadeIntents;
using static CnapLvivBot.DocumentDbSeeding.SeedValues.PreMadeResponses;

namespace CnapLvivBot.DocumentDbSeeding
{
    public class DocumentDbSeeder
    {
        private readonly DocumentClient _client;
        private readonly string _databaseName = ConfigurationManager.AppSettings["DatabaseName"];

        public DocumentDbSeeder()
        {
            _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["EndpointUrl"]), ConfigurationManager.AppSettings["PrimaryKey"]);
            _client.DeleteDatabaseIfExists(_databaseName).Wait();
            WriteLine($"Database {_databaseName} deleted");
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseName }).Wait();
            WriteLine($"Database {_databaseName} created");

            CreateCollectionsAsync().Wait();
            InitCollections().Wait();

        }

        private async Task CreateCollectionsAsync()
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Intent).Name });
            WriteLine($"Collection {typeof(Intent).Name} created");
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Response).Name });
            WriteLine($"Collection {typeof(Response).Name} created");

        }

        private async Task InitCollections()
        {
            await InitIntents();
            WriteLine($"Collection {typeof(Intent).Name} inited");

            await InitResponses();
            WriteLine($"Collection {typeof(Response).Name} inited");
        }


        private async Task InitIntents()
        {
            WriteLine();
            var intents = LoadIntents();

            foreach (var intent in intents)
            {
                await _client.CreateDocumentIfNotExists(intent, _databaseName);
                WriteLine($"{intent.id} inserted; ");
            }
            WriteLine();
        }

        private async Task InitResponses()
        {
            WriteLine();
            var responses = LoadResponses();

            foreach (var response in responses)
            {
                await _client.CreateDocumentIfNotExists(response, _databaseName);
                WriteLine($"{response.id} inserted; ");
            }
            WriteLine();
        }
    }
}