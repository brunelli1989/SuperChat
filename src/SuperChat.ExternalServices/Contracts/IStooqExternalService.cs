using SuperChat.ExternalServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperChat.ExternalServices.Contracts
{
    public interface IStooqExternalService
    {
        Task<IEnumerable<GetQuote.Response>> Get(GetQuote.Request request);
    }
}