using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Data.Infrastructure.Repository;
using Microsoft.Bot.Builder.Dialogs.Internals;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Data.Infrastructure
{
	public class ReplyBuilder
	{
		protected IRepository<Response> Repository;

		public ReplyBuilder()
		{
			switch (AppSettings["Source"])
			{
				case "document":
					Repository = new DocumentDbRepository<Response>();
					break;
				case "mongo":
					Repository = new MongoDbRepository<Response>();
					break;
				default:
					throw new Exception("Invalid source from Web.config.");
			}

		}

		public async Task<string> BuildReply(List<string> intents)
		{
			if (intents.Count <= 0) return "Сформулюйте своє питання по-іншому, будь ласка :)";
			intents = intents.Distinct().ToList();
			var fromDb = await Repository.GetAllAsync();
			var equityList = new Dictionary<Response, double>();
			foreach (var response in fromDb)
			{
				var equalElements = intents.Distinct().Intersect(response.Intents.Select(x => x.Content)).Count();
				var equivalence = (double)equalElements / Math.Max(response.Intents.Length, intents.Count);
				equityList.Add(key: response, value: equivalence);
			}

			if (equityList.Max(x => x.Value) > .7) // magic digit
			{
				return equityList.MaxBy(x => x.Value).Key.Content;
			}
			return "Сформулюйте своє питання по-іншому, будь ласка :)";
		}

	}
}
