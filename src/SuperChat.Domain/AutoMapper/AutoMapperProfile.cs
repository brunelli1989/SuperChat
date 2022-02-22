using AutoMapper;
using SuperChat.Domain.Commands;
using SuperChat.Domain.Events;
using SuperChat.ExternalServices.Models;

namespace SuperChat.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CalculateQuoteCommand, GetQuote.Request>();
            
            CreateMap<GetQuote.Response, QuoteCalculatedEvent>();
            
            CreateMap<CalculateQuoteCommand, QuoteCalculatedEvent>()
                .ForMember(x => x.Symbol, d => d.MapFrom(m => m.StockCode));
        }
    }
}
