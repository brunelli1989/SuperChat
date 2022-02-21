using SuperChat.Domain.Commands;
using System.Threading.Tasks;

namespace SuperChat.Domain.Contracts
{
    public interface IQuoteCalculator
    {
        Task CalculateQuote(CalculateQuoteCommand command);
    }
}