using AutoMapper;
using SuperChat.Domain.Commands;
using SuperChat.Domain.Contracts;
using SuperChat.Domain.Events;
using SuperChat.ExternalServices.Contracts;
using SuperChat.ExternalServices.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Domain.Services
{
    public class QuoteCalculator : IQuoteCalculator
    {
        private readonly IStooqExternalService _stooqExternalService;
        private readonly IMapper _mapper;
        private readonly IServiceBus _serviceBus;

        public QuoteCalculator(
            IStooqExternalService stooqExternalService,
            IMapper mapper,
            IServiceBus serviceBus
            )
        {
            _stooqExternalService = stooqExternalService;
            _mapper = mapper;
            _serviceBus = serviceBus;
        }

        public async Task CalculateQuote(CalculateQuoteCommand command)
        {
            var request = _mapper.Map<GetQuote.Request>(command);

            var response = await _stooqExternalService.Get(request);

            var first = response.FirstOrDefault();

            var quoteCalculatedEvent = _mapper.Map<QuoteCalculatedEvent>(first);

            _mapper.Map(command, quoteCalculatedEvent);

            await _serviceBus.Publish(quoteCalculatedEvent);
        }
    }
}