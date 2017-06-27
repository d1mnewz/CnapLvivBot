using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CnapLvivBot.Data.Entities;
using CnapLvivBot.Data.Infrastructure.Repository;

namespace CnapLvivBot.Data.Infrastructure
{
    public class ReplyBuilder
    {
        protected Repository<Response, int> Repository;

        public ReplyBuilder()
        {
            Repository = new Repository<Response, int>();
        }

        public async Task<string> BuildReply(List<string> intents)
        {
            // get all responses and define the most appropriate response where intents quite similar to intents that are passed as parameter

            var res = await Repository.GetAllAsync();
            return res.First().Content;
        }
    }
}
