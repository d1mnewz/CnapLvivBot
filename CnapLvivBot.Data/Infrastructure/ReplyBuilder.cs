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
        /// <exception cref="OverflowException">
        ///         <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
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

        /// <exception cref="OverflowException">The array is multidimensional and contains more than <see cref="F:System.Int32.MaxValue" /> elements.</exception>
        /// <exception cref="AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object. -or-An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions. </exception>
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
