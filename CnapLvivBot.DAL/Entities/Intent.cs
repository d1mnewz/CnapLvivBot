using System;

namespace CnapLvivBot.DAL.Entities
{
	[Serializable]
	public class Intent : BaseEntity
	{
		public string Content { get; set; }
	}
}