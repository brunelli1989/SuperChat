using AutoMapper;
using SuperChat.Web.Entities;
using SuperChat.Web.Models;

namespace SuperChat.Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Group, GroupViewModel>()
                .ReverseMap();
        }
    }
}