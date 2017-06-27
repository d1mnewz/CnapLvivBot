using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.DocumentDbSeeding.SeedValues;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
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
        private async Task CreateDocumentIfNotExists<T>(T entity) where T : BaseEntity
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseName, typeof(T).Name, entity.id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, typeof(T).Name), entity);

                }
                else
                {
                    throw;
                }
            }
        }

        private async Task InitIntents()
        {
            await CreateDocumentIfNotExists(Photo);
            await CreateDocumentIfNotExists(PreMadeIntents.Absense);
            await CreateDocumentIfNotExists(CNAP);
            await CreateDocumentIfNotExists(Kid12Years);
            await CreateDocumentIfNotExists(Circumstances);
            await CreateDocumentIfNotExists(Register);
            await CreateDocumentIfNotExists(DocumentsRequired);
            await CreateDocumentIfNotExists(Time);
            await CreateDocumentIfNotExists(Confirm);
            await CreateDocumentIfNotExists(Certificate13);
            await CreateDocumentIfNotExists(Where);
            await CreateDocumentIfNotExists(UkrainianPassport);
            await CreateDocumentIfNotExists(Price);
            await CreateDocumentIfNotExists(GivingDocuments);
            await CreateDocumentIfNotExists(ForeignPassport);
        }

        private async Task InitResponses()
        {
            await CreateDocumentIfNotExists(DocumentsForeignPassport);
            await CreateDocumentIfNotExists(DocumentsForeignPassportCnap);
            await CreateDocumentIfNotExists(DocumentsForeignPassportKid);
            await CreateDocumentIfNotExists(DocumentsRequiredCertifcate13);
            await CreateDocumentIfNotExists(PriceForeignPassport);
            await CreateDocumentIfNotExists(PriceForeignPassportKid);
            await CreateDocumentIfNotExists(UkrainianPassportKid);
            await CreateDocumentIfNotExists(UkrainianPassportChange);
            await CreateDocumentIfNotExists(WhereCertificate13);
            await CreateDocumentIfNotExists(WhereGiveDocumentsPassport);
            await CreateDocumentIfNotExists(RegisterAbsentPassport);
            await CreateDocumentIfNotExists(RegisterQueuePassport);
            await CreateDocumentIfNotExists(ForeignPassportTime);
            await CreateDocumentIfNotExists(PhotoCnap);
            await CreateDocumentIfNotExists(ConfirmRegisterForPassport);
        }

    }
}