using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using SuperChat.Domain.Commands;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusClient _serviceBusClient;
        private const string QUEUE_NAME = "calculate-quote";

        public ServiceBus(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task Send(CalculateQuoteCommand calculateQuoteCommand)
        {
            var sender = _serviceBusClient.CreateSender(QUEUE_NAME);

            var json = JsonConvert.SerializeObject(calculateQuoteCommand);

            var body = Encoding.UTF8.GetBytes(json);

            var message = new ServiceBusMessage(body);

            await sender.SendMessageAsync(message);

            await sender.DisposeAsync();
        }
    }
}