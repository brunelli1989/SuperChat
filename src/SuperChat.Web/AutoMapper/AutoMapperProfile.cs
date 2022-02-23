using AutoMapper;
using SuperChat.Web.Entities;
using SuperChat.Web.Events;
using SuperChat.Web.Models;

namespace SuperChat.Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Group, GroupViewModel>()
                .ReverseMap();

            CreateMap<Message, MessageViewModel>()
                .ReverseMap();

            CreateMap<MessageReceivedEvent, MessageViewModel>();
        }
    }
}