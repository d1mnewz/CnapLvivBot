using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CnapLvivBot.DAL.Entities;

namespace CnapLvivBot.DAL.Seeding
{
	public static class Intents
	{
		public static readonly Intent Kid12Years = new Intent {Content = "дитина до 12 років", Id = 1};
		public static readonly Intent Absense = new Intent {Content = "відсутність", Id = 2};
		public static readonly Intent Where = new Intent {Content = "де", Id = 3};
		public static readonly Intent Get = new Intent {Content = "взяти", Id = 4};
		public static readonly Intent CNAP = new Intent {Content = "цнап", Id = 5};
		public static readonly Intent Circumstances = new Intent {Content = "обставини для зміни", Id = 6};
		public static readonly Intent Register = new Intent {Content = "запис", Id = 7};
		public static readonly Intent DocumentsRequired = new Intent {Content = "потрібні документи", Id = 8};
		public static readonly Intent Time = new Intent {Content = "час", Id = 9};
		public static readonly Intent Confirm = new Intent {Content = "підтвердити", Id = 10};
		public static readonly Intent Certificate13 = new Intent {Content = "довідка 13", Id = 11};
		public static readonly Intent Photo = new Intent {Content = "фото", Id = 12};
		public static readonly Intent Price = new Intent {Content = "ціна", Id = 13};
		public static readonly Intent GivingDocuments = new Intent {Content = "подання документів", Id = 14};
		public static readonly Intent ForeignPassport = new Intent {Content = "закордонний паспорт", Id = 15};
		public static readonly Intent UkrainianPassport = new Intent {Content = "український паспорт", Id = 16};
		public static readonly Intent Passport = new Intent {Content = "паспорт", Id = 17};
		public static readonly Intent Pay = new Intent {Content = "оплатити", Id = 18};
		public static readonly Intent Requisites = new Intent {Content = "реквізити", Id = 19};
		public static readonly Intent Not = new Intent {Content = "окрім", Id = 20};

		public static readonly Intent Start = new Intent {Content = "/start", Id = 21};

		public static IEnumerable<Intent> LoadIntents()
		{
			var type = typeof(Intents);
			return type.GetFields(BindingFlags.Static | BindingFlags.Public).Select(p => p.GetValue(null) as Intent).ToList();
		}
	}
}