using System.Net;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CnapLvivBot.Seeding.DocumentDb.Extensions
{
    public static class DocumentClientExtensions
    {
        public static async Task CreateDocumentIfNotExistsAsync<T>(this DocumentClient client, T entity, string databaseName) where T : BaseEntity
        {
            try
            {
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, typeof(T).Name, entity.id)).ConfigureAwait(false);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, typeof(T).Name), entity).ConfigureAwait(false);

                }
                else
                {
                    throw;
                }
            }
        }

        /// <exception cref="DocumentClientException"></exception>
        public static async Task DeleteDatabaseIfExistsAsync(this DocumentClient client, string databaseName)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName)).ConfigureAwait(false);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    return;
                }
                throw;
            }
            await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName)).ConfigureAwait(false);
        }
    }
}