using System.Collections.Generic;
using System.Linq;
using com.valgut.libs.bots.Wit;
using JetBrains.Annotations;
using Microsoft.Bot.Connector;

namespace CnapLvivBot.Data
{
    public static class AiHandler
    {
        [NotNull]
        public static List<string> GetIntentsList(Activity activity, string aiKey)
        {
            var wit = new WitClient(aiKey);
            var msg = wit.Converse(activity.From.Id, activity.Text);

            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return msg?.entities["intent"].Select(x => x.value.ToString()).ToList();
            }
            catch (KeyNotFoundException)
            {
                return new List<string>();
            }
        }
    }

}