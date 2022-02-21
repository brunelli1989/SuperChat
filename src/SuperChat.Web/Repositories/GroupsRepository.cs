using SuperChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperChat.Web.Repositories
{
    public interface IGroupsRepository
    {
        void Add(GroupViewModel group);
        GroupViewModel Get(Guid id);
        List<GroupViewModel> Get();
        void Remove(Guid id);
    }

    public class GroupsRepository : IGroupsRepository
    {
        private static List<GroupViewModel> _groups = new List<GroupViewModel>
        {
            new GroupViewModel
            {
                Name = "Group 1"
            },
            new GroupViewModel
            {
                Name = "Group 2"
            }
        };

        public void Add(GroupViewModel group)
        {
            _groups.Add(group);
        }

        public GroupViewModel Get(Guid id)
        {
            return _groups.FirstOrDefault(x => x.Id == id);
        }

        public List<GroupViewModel> Get()
        {
            return _groups;
        }

        public void Remove(Guid id)
        {
            _groups.RemoveAll(x => x.Id == id);
        }
    }
}