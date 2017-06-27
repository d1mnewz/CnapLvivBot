namespace CnapLvivBot.Data.Entities
{
    public class Response : BaseEntity
    {

        public string Content { get; set; }

        public Intent[] Intents { get; set; } // maybe just array of strings, doesnt matter

    }
}