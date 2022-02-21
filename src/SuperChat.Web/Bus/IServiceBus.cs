using SuperChat.Domain.Commands;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public interface IServiceBus
    {
        Task Send(CalculateQuoteCommand calculateQuoteCommand);
    }
}