using System;
using System.Threading.Tasks;
using CnapLvivBot.BusinessLogic;
using CnapLvivBot.DAL.Infrastructure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CnapLvivBot.Dialogs
{
	[Serializable]
	public class RootDialog : IDialog<object>
	{
		private readonly ILanguageRecognitionTool _languageRecognitionTool;
		private readonly IReplyBuilder _replyBuilder;

		public RootDialog(ILanguageRecognitionTool languageRecognitionTool, IReplyBuilder replyBuilder)
		{
			_languageRecognitionTool = languageRecognitionTool;
			_replyBuilder = replyBuilder;
		}

		public Task StartAsync(IDialogContext context)
		{
			context.Wait(MessageReceivedAsync);
			return Task.CompletedTask;
		}

		private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
		{
			var activity = await result as Activity;
			var intents = _languageRecognitionTool?.GetIntentsFromMessage(activity?.From.Id, activity?.Text);
			var response = _replyBuilder.BuildReply(intents);

			await context.PostAsync(response);

			context.Wait(MessageReceivedAsync);
		}
	}
}