using System;
using System.Configuration;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.DocumentDbSeeding.Extensions;
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
            _client.DeleteDatabaseIfExists(_databaseName).Wait();
            Console.WriteLine($"Database {_databaseName} deleted");
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseName }).Wait();
            Console.WriteLine($"Database {_databaseName} created");

            CreateCollectionsAsync().Wait();
            InitCollections().Wait();

        }

        private async Task CreateCollectionsAsync()
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Intent).Name });
            Console.WriteLine($"Collection {typeof(Intent).Name} created");
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseName), new DocumentCollection { Id = typeof(Response).Name });
            Console.WriteLine($"Collection {typeof(Response).Name} created");

        }

        private async Task InitCollections()
        {
            await InitIntents();
            Console.WriteLine($"Collection {typeof(Intent).Name} inited");

            await InitResponses();
            Console.WriteLine($"Collection {typeof(Response).Name} inited");



        }


        private async Task InitIntents()
        {
            await _client.CreateDocumentIfNotExists(Photo, _databaseName);
            await _client.CreateDocumentIfNotExists(Absense, _databaseName);
            await _client.CreateDocumentIfNotExists(CNAP, _databaseName);
            await _client.CreateDocumentIfNotExists(Kid12Years, _databaseName);
            await _client.CreateDocumentIfNotExists(Circumstances, _databaseName);
            await _client.CreateDocumentIfNotExists(Register, _databaseName);
            await _client.CreateDocumentIfNotExists(DocumentsRequired, _databaseName);
            await _client.CreateDocumentIfNotExists(Time, _databaseName);
            await _client.CreateDocumentIfNotExists(Confirm, _databaseName);
            await _client.CreateDocumentIfNotExists(Certificate13, _databaseName);
            await _client.CreateDocumentIfNotExists(Where, _databaseName);
            await _client.CreateDocumentIfNotExists(UkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(Price, _databaseName);
            await _client.CreateDocumentIfNotExists(GivingDocuments, _databaseName);
            await _client.CreateDocumentIfNotExists(ForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(Passport, _databaseName);
            await _client.CreateDocumentIfNotExists(Pay, _databaseName);
            await _client.CreateDocumentIfNotExists(Requisites, _databaseName);
            await _client.CreateDocumentIfNotExists(Not, _databaseName);


            await _client.CreateDocumentIfNotExists(Start, _databaseName);


        }

        private async Task InitResponses()
        {
            await _client.CreateDocumentIfNotExists(DocumentsForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(DocumentsForeignPassportCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(DocumentsForeignPassportKid, _databaseName);
            await _client.CreateDocumentIfNotExists(DocumentsRequiredCertifcate13, _databaseName);
            await _client.CreateDocumentIfNotExists(PriceForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(PriceForeignPassportKid, _databaseName);
            await _client.CreateDocumentIfNotExists(UkrainianPassportKid, _databaseName);
            await _client.CreateDocumentIfNotExists(UkrainianPassportChange, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereCertificate13, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereGiveDocumentsForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereGiveDocumentsUkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(RegisterAbsentPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(ForeignPassportTime, _databaseName);
            await _client.CreateDocumentIfNotExists(ForeignPassportTimeKid, _databaseName);
            await _client.CreateDocumentIfNotExists(PhotoCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(PhotoCnapForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(PhotoCnapUkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(ConfirmRegisterForPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(WherePassportNotCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereUkrainianPassportNotCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereForeignPassportNotCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(RequisitesForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(RequisitesUkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(RegisterCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToRegister, _databaseName);
            await _client.CreateDocumentIfNotExists(HowToRegister, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereGiveDocumentsForeignPassport1, _databaseName);
            await _client.CreateDocumentIfNotExists(UkrainianPassportKid1, _databaseName);
            await _client.CreateDocumentIfNotExists(DocumentsForeignPassport1, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToPayForPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToPayForForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToPay, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToPayForUkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(PassportGeneralizedPrice, _databaseName);
            await _client.CreateDocumentIfNotExists(PassportGeneralizedTime, _databaseName);
            await _client.CreateDocumentIfNotExists(HowToRegisterPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(HowToRegisterPassportCnap, _databaseName);
            await _client.CreateDocumentIfNotExists(HowToRegisterForeignPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(HowToRegisterUkrainianPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(WhereToRegisterPassport, _databaseName);
            await _client.CreateDocumentIfNotExists(RequisitesPassport, _databaseName);

            await _client.CreateDocumentIfNotExists(StartResponse, _databaseName);
        }

    }
}