using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Seeding.MongoDb.Extensions;
using MongoDB.Driver;
using static CnapLvivBot.Seeding.SeedValues.PreMadeIntents;
using static CnapLvivBot.Seeding.SeedValues.PreMadeResponses;

namespace CnapLvivBot.Seeding.MongoDb
{
	public class MongoDbDriver : IDriver
	{
		protected readonly MongoClient Client;

		public MongoDbDriver()
		{
			Client = Client.GetClient();
		}
		public async Task RunAsync()
		{
			var targetDb = Client.GetDatabase("cnap");

			await targetDb.DropCollectionAsync(typeof(Intent).Name).ConfigureAwait(false);
			await targetDb.DropCollectionAsync(typeof(Response).Name).ConfigureAwait(false);

			await targetDb.CreateCollectionAsync(typeof(Intent).Name).ConfigureAwait(false);
			await targetDb.CreateCollectionAsync(typeof(Response).Name).ConfigureAwait(false);

			var intents = targetDb.GetCollection<Intent>(typeof(Intent).Name);
			var responses = targetDb.GetCollection<Response>(typeof(Response).Name);

			await intents.InsertManyAsync(documents: LoadIntents()).ConfigureAwait(false);
			await responses.InsertManyAsync(documents: LoadResponses()).ConfigureAwait(false);
		}
	}
}
