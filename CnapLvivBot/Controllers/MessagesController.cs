using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using CnapLvivBot.Data.Infrastructure;
using Microsoft.Bot.Connector;
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
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                Activity reply = activity.CreateReply();

                reply.Text = new ReplyBuilder().BuildReply(
                    GetIntentsList(activity, WebConfigurationManager.AppSettings["WitClientKey"]));

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}