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
                var equityList = new Dictionary<Response, double>();
                foreach (var response in fromDb)
                {

                    var equalElements = intents.Intersect(response.Intents.Select(x => x.Content)).Count();
                    var equivalence = (double) equalElements / Math.Max(response.Intents.Length, intents.Count);
                    equityList.Add(key: response, value: equivalence);
                }

                if (equityList.Max(x => x.Value) > .6) // magic digit
                {
                    return equityList.MaxBy(x => x.Value).Key.Content;
                }
            }
            return "Сформулюйте своє питання по-іншому, будь ласка :)";
        }

    }
}
