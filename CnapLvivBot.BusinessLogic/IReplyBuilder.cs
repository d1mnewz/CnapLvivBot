using System.Collections.Generic;

namespace CnapLvivBot.BusinessLogic
{
	public interface IReplyBuilder
	{
		string BuildReply(IList<string> intents);
	}
}