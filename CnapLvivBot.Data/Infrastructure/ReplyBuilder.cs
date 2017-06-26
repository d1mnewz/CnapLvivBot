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

            var res = await Repository.GetAllAsync();
            return res.First().Content;
            // get from repo and compare to 'intents'
            //Repository<Response> repository = new Repository<Response>();
            //repository.GetAll();
        }
    }
}
