using System;
using System.Collections.Generic;
using System.Linq;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Data.Infrastructure.Repository;
using Microsoft.Bot.Builder.Dialogs.Internals;

namespace CnapLvivBot.Data.Infrastructure
{
    public class ReplyBuilder
    {
        protected Repository<Response> Repository;

        public ReplyBuilder()
        {
            Repository = new Repository<Response>();
        }

        public string BuildReply(List<string> intents)
        {
            if (intents.Count > 0)
            {
                var fromDb = Repository.GetAllAsync().Result;
                var equityList = new List<(Response response, double equityPercent)>();
                foreach (var response in fromDb)
                {

                    int equalElements = intents.Intersect(response.Intents.Select(x => x.Content)).Count();
                    double equivalence = (double) equalElements / Math.Max(response.Intents.Length, intents.Count);
                    equityList.Add((response, equivalence));
                }

                if (equityList.Max(x => x.equityPercent) > .6) // magic digit
                {
                    return equityList.MaxBy(x => x.equityPercent).response.Content;
                }
            }
            return "Сформулюйте своє питання по-іншому, будь ласка :)";
        }

    }
}
