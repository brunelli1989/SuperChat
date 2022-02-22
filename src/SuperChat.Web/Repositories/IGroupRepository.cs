using SuperChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperChat.Web.Repositories
{
    public interface IGroupRepository
    {
        Task Add(GroupViewModel viewModel);
        Task Update(GroupViewModel viewModel);
        Task<GroupViewModel> Get(Guid id);
        Task<List<GroupViewModel>> Get();
        Task Remove(Guid id);
        Task Commit();
    }
}