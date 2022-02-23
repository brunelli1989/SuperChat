using Azure.Messaging.ServiceBus;
using SuperChat.Domain.Commands;
using SuperChat.Web.Events;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusClient _serviceBusClient;
        private const string CALCULATE_QUOTE_QUEUE_NAME = "calculate-quote";
        private const string MESSAGE_RECEIVED_QUEUE_NAME = "message-received";

        public ServiceBus(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task Send(CalculateQuoteCommand command)
        {
            var sender = _serviceBusClient.CreateSender(CALCULATE_QUOTE_QUEUE_NAME);

            var message = new ServiceBusMessage(new BinaryData(command));

            await sender.SendMessageAsync(message);

            await sender.DisposeAsync();
        }

        public async Task Publish(MessageReceivedEvent messageReceived)
        {
            var sender = _serviceBusClient.CreateSender(MESSAGE_RECEIVED_QUEUE_NAME);

            var message = new ServiceBusMessage(new BinaryData(messageReceived));

            await sender.SendMessageAsync(message);

            await sender.DisposeAsync();
        }
    }
}