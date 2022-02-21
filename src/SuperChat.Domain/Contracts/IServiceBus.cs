using SuperChat.Domain.Events;
using System.Threading.Tasks;

namespace SuperChat.Domain.Contracts
{
    public interface IServiceBus
    {
        Task Publish(QuoteCalculatedEvent quoteCalculatedEvent);
    }
}