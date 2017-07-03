using System.Net;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CnapLvivBot.DocumentDbSeeding.Extensions
{
    public static class DocumentClientExtensions
    {
        public static async Task CreateDocumentIfNotExists<T>(this DocumentClient client, T entity, string databaseName) where T : BaseEntity
        {
            try
            {
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, typeof(T).Name, entity.id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, typeof(T).Name), entity);

                }
                else
                {
                    throw;
                }
            }
        }
        public static async Task DeleteDatabaseIfExists(this DocumentClient client, string databaseName)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));

            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    return;
                }
                throw;
            }
            await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
        }
    }
}