namespace CnapLvivBot.Data.Entities
{
    public class Response
    {
        // ReSharper disable once InconsistentNaming
        public string id { get; set; }
        public string Content { get; set; }

        public Intent[] Intents { get; set; } // maybe just array of strings, doesnt matter

    }
}