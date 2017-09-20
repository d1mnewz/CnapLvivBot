using System;
using CnapLvivBot.Seeding.DocumentDb;
using CnapLvivBot.Seeding.MongoDb;

namespace CnapLvivBot.Seeding
{
	public static class Program
	{
		public static void Main()
		{
			Console.WriteLine("Switch function: mongo - use MongoDB at cloud server, document - use cloud DocumentDb");
			var selection = Console.ReadLine();
			IDriver driver = null;
			switch (selection)
			{
				case "mongo":
					driver = new MongoDbDriver();
					break;
				case "document":
					driver = new DocumentDbDriver();
					break;
				default:
					Console.WriteLine("Driver not found");
					break;
			}
			driver?.RunAsync().Wait();

			Console.WriteLine("Done. press any key to exit");
			Console.ReadLine();
		}
	}
}
