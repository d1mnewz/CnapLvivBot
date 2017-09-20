using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CnapLvivBot.Data.Infrastructure;
using Microsoft.Bot.Connector;
using static System.Web.Configuration.WebConfigurationManager;
using static CnapLvivBot.Data.AiHandler;

namespace CnapLvivBot.Controllers
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
		{
			if (activity.Type == ActivityTypes.Message)
			{
				var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

				var reply = activity.CreateReply();

				reply.Text = await new ReplyBuilder().BuildReply(
					GetIntentsList(activity, AppSettings["WitClientKey"]));

				await connector.Conversations.ReplyToActivityAsync(reply);
			}
			var response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}
	}
}