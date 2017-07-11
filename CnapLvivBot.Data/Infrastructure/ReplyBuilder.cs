using System;
using System.Collections.Generic;
using System.Linq;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Data.Infrastructure.Repository;
using Microsoft.Bot.Builder.Dialogs.Internals;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Data.Infrastructure
{
    public class ReplyBuilder
    {
        protected IRepository<Response> Repository;

        /// <exception cref="Exception">Invalid source from Web.config.</exception>
        /// <exception cref="NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
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

        public string BuildReply(List<string> intents)
        {
            if (intents.Count > 0)
            {
                var fromDb = Repository.GetAllAsync().Result;
                var equityList = new Dictionary<Response, double>();
                foreach (var response in fromDb)
                {

                    var equalElements = intents.Intersect(response.Intents.Select(x => x.Content)).Count();
                    var equivalence = (double)equalElements / Math.Max(response.Intents.Length, intents.Count);
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
