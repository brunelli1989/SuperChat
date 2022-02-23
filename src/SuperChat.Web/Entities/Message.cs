using System;

namespace SuperChat.Web.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}