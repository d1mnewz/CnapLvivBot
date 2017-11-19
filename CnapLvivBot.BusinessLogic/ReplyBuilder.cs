using System;
using System.Collections.Generic;
using System.Linq;
using CnapLvivBot.BusinessLogic.Extensions;
using CnapLvivBot.DAL;
using CnapLvivBot.DAL.Entities;
using CnapLvivBot.DAL.Infrastructure;
using CnapLvivBot.DAL.Seeding;

namespace CnapLvivBot.BusinessLogic
{
	[Serializable]
	public class ReplyBuilder : IReplyBuilder
	{
		private readonly IRepository<Response> _responseRepository;

		public ReplyBuilder(string connectionString)
		{
			_responseRepository = new RedisRepository<Response>(connectionString);
		}

		public string BuildReply(IList<string> intents)
		{
			if (intents.Count <= 0) return Response.Default;
			intents = intents.Distinct().ToList();
			var fromDb = /*_responseRepository.GetAll(); */Responses.LoadResponses();
			var equityList = new Dictionary<Response, double>();
			foreach (var response in fromDb)
			{
				var equalElements = intents.Distinct().Intersect(response.Intents.Select(x => x.Content)).Count();
				var equivalence = (double)equalElements / Math.Max(response.Intents.Count, intents.Count);
				equityList.Add(response, equivalence);
			}

			return equityList.Max(x => x.Value) > .7 ? equityList.MaxBy(x => x.Value).Key?.Content : Response.Default;
		}
	}
}