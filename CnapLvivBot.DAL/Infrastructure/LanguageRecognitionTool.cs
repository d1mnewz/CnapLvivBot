using System.Collections.Generic;
using System.Linq;
using com.valgut.libs.bots.Wit;

namespace CnapLvivBot.DAL.Infrastructure
{
	public class LanguageRecognitionTool : ILanguageRecognitionTool
	{
		protected WitClient Client { get; set; }

		public LanguageRecognitionTool(string aiKey)
		{
			Client = new WitClient(aiKey);
		}

		public IList<string> GetIntentsFromMessage(string fromId, string text)
		{
			var msg = Client.Converse(fromId, text);
			try
			{
				return msg?.entities["intent"].Select(x => x.value.ToString()).ToList();
			}
			catch (KeyNotFoundException)
			{
				return new List<string>();
			}
		}

	}

	public interface ILanguageRecognitionTool
	{
		IList<string> GetIntentsFromMessage(string fromId, string text);
	}
}
