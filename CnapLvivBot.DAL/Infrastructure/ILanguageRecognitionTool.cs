using System.Collections.Generic;

namespace CnapLvivBot.DAL.Infrastructure
{
	public interface ILanguageRecognitionTool
	{
		IList<string> GetIntentsFromMessage(string fromId, string text);
	}
}