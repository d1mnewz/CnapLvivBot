using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CnapLvivBot.BusinessLogic.Extensions;
using CnapLvivBot.DAL;
using CnapLvivBot.DAL.Infrastructure;

namespace CnapLvivBot.BusinessLogic
{
	public class ReplyBuilder
	{
		protected readonly IRepository<Response> Repository;

		public ReplyBuilder()
		{
			Repository = null; // TODO: connect SQL/redis and write repository
		}

		public async Task<string> BuildReply(List<string> intents)
		{
			if (intents.Count <= 0) return Response.Default;
			intents = intents.Distinct().ToList();
			var fromDb = await Repository.GetAllAsync();
			var equityList = new Dictionary<Response, double>();
			foreach (var response in fromDb)
			{
				var equalElements = intents.Distinct().Intersect(response.Intents.Select(x => x.Content)).Count();
				var equivalence = (double)equalElements / Math.Max(response.Intents.Count, intents.Count);
				equityList.Add(key: response, value: equivalence);
			}

			return equityList.Max(x => x.Value) > .7 ? 
				equityList.MaxBy(x => x.Value).Key.Content : Response.Default;
		}

	}

}