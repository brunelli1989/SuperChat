using System;

namespace SuperChat.Web.Events
{
    public class MessageReceivedEvent
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid GroupId { get; set; }

        public MessageReceivedEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}