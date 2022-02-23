using SuperChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperChat.Web.Repositories
{
    public interface IMessageRepository
    {
        Task Add(MessageViewModel viewModel);
        Task<List<MessageViewModel>> Get(Guid groupId);
        Task Commit();
    }
}