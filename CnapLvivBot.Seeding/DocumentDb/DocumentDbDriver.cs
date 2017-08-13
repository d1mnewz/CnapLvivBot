using System;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Seeding.DocumentDb.Extensions;
using CnapLvivBot.Seeding.SeedValues;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Seeding.DocumentDb
{
    public class DocumentDbDriver : IDriver
    {
        private readonly DocumentClient _client;
        private readonly string _databaseName = AppSettings["DatabaseName"];

        
        public DocumentDbDriver()
        {
            _client = new DocumentClient(serviceEndpoint: new Uri(uriString: AppSettings["DocumentDbEndpointUrl"]), authKeyOrResourceToken: AppSettings["DocumentDbPrimaryKey"]);

        }

        /// <exception cref="DocumentClientException">This exception can encapsulate many different types of errors. To determine the specific error always look at the StatusCode property.</exception>
        /// <exception cref="AggregateException">Represents a consolidation of failures that occured during async processing. Look within InnerExceptions to find the actual exception(s).</exception>
        /// <exception cref="ArgumentNullException">If <paramref>
        ///         <name>database</name>
        ///     </paramref>
        ///     is not set.</exception>
        public async Task RunAsync()
        {
            await _client.DeleteDatabaseIfExistsAsync(_databaseName).ConfigureAwait(false);
            Console.WriteLine($"Database {_databaseName} deleted");
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseName }).ConfigureAwait(false);

            Console.WriteLine($"Database {_databaseName} created");

            await CreateCollectionsAsync().ConfigureAwait(false);
            await InitCollectionsAsync().ConfigureAwait(false);
        }

        private async Task CreateCollectionsAsync()
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Intent).Name }).ConfigureAwait(false);
            Console.WriteLine($"Collection {typeof(Intent).Name} created");
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Response).Name }).ConfigureAwait(false);
            Console.WriteLine($"Collection {typeof(Response).Name} created");

        }

        private async Task InitCollectionsAsync()
        {
            await InitIntentsAsync().ConfigureAwait(false);
            Console.WriteLine($"Collection {typeof(Intent).Name} inited");

            await InitResponsesAsync().ConfigureAwait(false);
            Console.WriteLine($"Collection {typeof(Response).Name} inited");
        }


        private async Task InitIntentsAsync()
        {
            Console.WriteLine();
            var intents = PreMadeIntents.LoadIntents();

            foreach (var intent in intents)
            {
                await _client.CreateDocumentIfNotExistsAsync(intent, _databaseName).ConfigureAwait(false);
                Console.WriteLine($"{intent.id} intent inserted; ");
            }
            Console.WriteLine();
        }

        private async Task InitResponsesAsync()
        {
            Console.WriteLine();
            var responses = PreMadeResponses.LoadResponses();

            foreach (var response in responses)
            {
                await _client.CreateDocumentIfNotExistsAsync(response, _databaseName).ConfigureAwait(false);
                Console.WriteLine($"{response.id} response inserted; ");
            }
            Console.WriteLine();
        }


    }
}