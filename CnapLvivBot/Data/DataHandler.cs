using System;
using System.Collections.Generic;
using System.Linq;
using com.valgut.libs.bots.Wit;
using JetBrains.Annotations;
using Microsoft.Bot.Connector;

namespace CnapLvivBot.Data
{
    public static class DataHandler
    {
        public static string GetReplyFromDb(string intent, сnapEntities Db)
        {
            var arrToRandomFrom = Db.Responses.Where(x => x.Intent.Content == intent).ToArray();
            if (arrToRandomFrom.Length > 0)
            {
                return arrToRandomFrom[new Random().Next(arrToRandomFrom.Length)].Content;
            }

            return Db.Responses.Where(x => x.Intent.Content == "noreply").ToArray()[
                new Random().Next(Db.Responses.Where(x => x.Intent.Content == "noreply").ToArray().Length)].Content;
        }

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