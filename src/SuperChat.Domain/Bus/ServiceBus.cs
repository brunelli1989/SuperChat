using Azure.Messaging.ServiceBus;
using SuperChat.Domain.Contracts;
using SuperChat.Domain.Events;
using System;
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

            var message = new ServiceBusMessage(new BinaryData(quoteCalculatedEvent));

            await sender.SendMessageAsync(message);

            await sender.DisposeAsync();
        }
    }
}