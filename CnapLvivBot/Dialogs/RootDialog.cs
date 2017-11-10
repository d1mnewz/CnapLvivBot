using System;
using System.Threading.Tasks;
using CnapLvivBot.BusinessLogic;
using CnapLvivBot.DAL.Infrastructure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using static System.Configuration.ConfigurationManager;

namespace CnapLvivBot.Dialogs
{
	[Serializable]
	public class RootDialog : IDialog<object>
	{
		private ILanguageRecognitionTool _languageRecognitionTool;
		private IReplyBuilder _replyBuilder;


		public Task StartAsync(IDialogContext context)
		{
			_replyBuilder = new ReplyBuilder(ConnectionStrings["Redis"].ConnectionString);
			_languageRecognitionTool =
				new LanguageRecognitionTool();

			return Task.CompletedTask;
		}

		private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
		{
			var activity = await result as Activity;
			var dummy = _languageRecognitionTool?.GetIntentsFromMessage(activity?.From.Id, activity?.Text);
			// calculate something for us to return
			var length = (activity?.Text ?? string.Empty).Length;

			// return our reply to the user
			await context.PostAsync($"You sent {activity.Text} which was {length} characters");

			context.Wait(MessageReceivedAsync);
		}
	}
}