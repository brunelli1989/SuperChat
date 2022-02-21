using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using SuperChat.Domain.Contracts;
using SuperChat.Domain.Events;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Domain.Bus
{
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusClient _serviceBusClient;
        private const string QUEUE_NAME = "quote-calculated";

        public ServiceBus(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task Publish(QuoteCalculatedEvent quoteCalculatedEvent)
        {
            var sender = _serviceBusClient.CreateSender(QUEUE_NAME);

            var json = JsonConvert.SerializeObject(quoteCalculatedEvent);

            var body = Encoding.UTF8.GetBytes(json);

            var message = new ServiceBusMessage(body);

            await sender.SendMessageAsync(message);

            await sender.DisposeAsync();
        }
    }
}