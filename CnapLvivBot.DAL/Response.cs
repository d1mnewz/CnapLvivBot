﻿using System.Collections.Generic;

namespace CnapLvivBot.DAL
{
	public class Response : BaseEntity
	{
		public string Content { get; set; }
		public IList<Intent> Intents { get; set; }
		public static string Default => "Сформулюйте своє питання по-іншому, будь ласка :)";
	}
}