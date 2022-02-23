using System;

namespace SuperChat.Web.Models
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid GroupId { get; set; }
        public string UserName { get; set; }
    }
}