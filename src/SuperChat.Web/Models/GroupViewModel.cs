using System;

namespace SuperChat.Web.Models
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public GroupViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}