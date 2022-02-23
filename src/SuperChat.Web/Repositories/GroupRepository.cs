using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperChat.Web.Data;
using SuperChat.Web.Entities;
using SuperChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperChat.Web.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GroupRepository(
            ApplicationDbContext applicationDbContext,
            IMapper mapper
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task Add(GroupViewModel viewModel)
        {
            var entity = _mapper.Map<Group>(viewModel);
            await _applicationDbContext.AddAsync(entity);
        }

        public async Task<GroupViewModel> Get(Guid id)
        {
            var entity = await _applicationDbContext
                .Set<Group>()
                .FirstOrDefaultAsync(x => x.Id == id);

            var viewModel = _mapper.Map<GroupViewModel>(entity);

            return viewModel;
        }

        public async Task Update(GroupViewModel viewModel)
        {
            var entity = await _applicationDbContext
                .Set<Group>()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            _mapper.Map(viewModel, entity);
        }

        public async Task<List<GroupViewModel>> Get()
        {
            var entities = await _applicationDbContext
                .Set<Group>()
                .ToListAsync();

            var viewModels = _mapper.Map<List<GroupViewModel>>(entities);

            return viewModels;
        }

        public async Task Remove(Guid id)
        {
            var entity = await _applicationDbContext.Set<Group>().FindAsync(id);
            _applicationDbContext.Set<Group>().Remove(entity);
        }

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}