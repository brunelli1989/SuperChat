using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperChat.Web.Data;
using SuperChat.Web.Entities;
using SuperChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Web.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public MessageRepository(
            ApplicationDbContext applicationDbContext,
            IMapper mapper
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task Add(MessageViewModel viewModel)
        {
            var entity = _mapper.Map<Message>(viewModel);
            await _applicationDbContext.AddAsync(entity);
        }

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<MessageViewModel>> Get(Guid groupId)
        {
            var entities = await _applicationDbContext
                .Set<Message>()
                .Where(x => x.GroupId == groupId)
                .OrderBy(x => x.Date)
                .Take(50)
                .ToListAsync();

            var viewModels = _mapper.Map<List<MessageViewModel>>(entities);

            return viewModels;
        }
    }
}