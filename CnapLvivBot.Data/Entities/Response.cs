namespace CnapLvivBot.Data.Entities
{
    public class Response
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public Intent[] Intents { get; set; } // maybe just array of strings, doesnt matter

    }
}