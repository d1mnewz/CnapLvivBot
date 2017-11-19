using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CnapLvivBot.DAL;
using CnapLvivBot.DAL.Entities;
using CnapLvivBot.DAL.Infrastructure;
using CnapLvivBot.DAL.Seeding;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Controllers
{
	public class DataController : ApiController
	{
		private readonly IRepository<Intent> _intentsRepository;
		private readonly IRepository<Response> _responsesRepository;

		protected DataController()
		{
			_responsesRepository = new RedisRepository<Response>(ConnectionStrings["Redis"].ConnectionString);
			_intentsRepository = new RedisRepository<Intent>(ConnectionStrings["Redis"].ConnectionString);
		}

		[HttpPost]
		public async Task ReloadData(bool reCreate = false)
		{
			var intents = Intents.LoadIntents();
			var responses = Responses.LoadResponses();
			if (reCreate)
			{
				foreach (var intent in intents)
					_intentsRepository.Remove(intent.Id);
				foreach (var response in responses)
					_responsesRepository.Remove(response.Id);
			}
			_intentsRepository.Add(intents.ToList());
			_responsesRepository.Add(responses.ToList());
		}
	}
}