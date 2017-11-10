using System;
using System.Collections.Generic;
using System.Linq;
using com.valgut.libs.bots.Wit;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.DAL.Infrastructure
{
	[Serializable]
	public class LanguageRecognitionTool : ILanguageRecognitionTool
	{
		public LanguageRecognitionTool()
		{
			Client = new WitClient(ConnectionStrings["WitAiKey"].ConnectionString);
		}

		[NonSerialized] protected WitClient Client;

		public IList<string> GetIntentsFromMessage(string fromId, string text)
		{
			if (Client is null)
				this.Client = new WitClient(ConnectionStrings["WitAiKey"].ConnectionString);

			try
			{
				return Client.Converse(fromId, text)?.entities["intent"].Select(x => x.value.ToString()).ToList();
			}
			catch (KeyNotFoundException)
			{
				return new List<string>();
			}
		}
	}
}