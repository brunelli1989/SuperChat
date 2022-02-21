using AutoMapper;
using Moq;
using SuperChat.Domain.Commands;
using SuperChat.Domain.Contracts;
using SuperChat.Domain.Events;
using SuperChat.Domain.Services;
using SuperChat.ExternalServices.Contracts;
using SuperChat.ExternalServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SuperChat.Test.Services
{
    public class QuoteCalculatorTest
    {
        private readonly Mock<IStooqExternalService> _stooqExternalService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IServiceBus> _serviceBus;
        private readonly QuoteCalculator _quoteCalculator;

        public QuoteCalculatorTest()
        {
            _stooqExternalService = new Mock<IStooqExternalService>();
            _mapper = new Mock<IMapper>();
            _serviceBus = new Mock<IServiceBus>();
            _quoteCalculator = new QuoteCalculator(_stooqExternalService.Object, _mapper.Object, _serviceBus.Object);
        }

        [Fact]
        public async Task QuoteCalculator_CalculateQuote_Success()
        {
            var command = new CalculateQuoteCommand { };

            _mapper.Setup(x => x.Map<GetQuote.Request>(It.IsAny<CalculateQuoteCommand>()))
                .Returns(new GetQuote.Request { });

            var list = new List<GetQuote.Response>
            {
                new GetQuote.Response { }
            };

            _mapper.Setup(x => x.Map<QuoteCalculatedEvent>(It.IsAny<GetQuote.Response>()))
                .Returns(new QuoteCalculatedEvent { });

            _stooqExternalService.Setup(x => x.Get(It.IsAny<GetQuote.Request>()))
                .Returns(Task.FromResult(list));

            await _quoteCalculator.CalculateQuote(command);

            _serviceBus.Verify(x => x.Publish(It.IsAny<QuoteCalculatedEvent>()), Times.Once);
        }
    }
}