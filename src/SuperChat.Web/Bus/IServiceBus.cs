using SuperChat.Domain.Commands;
using SuperChat.Web.Events;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public interface IServiceBus
    {
        Task Send(CalculateQuoteCommand calculateQuoteCommand);
        Task Publish(MessageReceivedEvent messageReceived);
    }
}