using System.Collections.Generic;
using System.Threading.Tasks;

namespace CnapLvivBot.BusinessLogic
{
	public interface IReplyBuilder
	{
		Task<string> BuildReply(IList<string> intents);
	}
}