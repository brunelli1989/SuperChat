using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.SignalR;
using SuperChat.Domain.Events;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public class QuoteCalculatedServiceBusHostedService : AbstractServiceBusHostedService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private const string QUEUE_NAME = "quote-calculated";

        public QuoteCalculatedServiceBusHostedService(ServiceBusClient serviceBusClient, IHubContext<ChatHub> hubContext)
            : base(serviceBusClient, QUEUE_NAME)
        {
            _hubContext = hubContext;
        }

        public override async Task MessageHandler(ProcessMessageEventArgs args)
        {
            Console.WriteLine("Handle message");

            var quoteCalculatedEvent = args.Message.Body.ToObjectFromJson<QuoteCalculatedEvent>();

            var client = _hubContext.Clients.Client(quoteCalculatedEvent.CorrelationId);

            string message = quoteCalculatedEvent.Success
                ? $"{quoteCalculatedEvent.Symbol} quote is ${quoteCalculatedEvent.High} per share"
                : $"Symbol {quoteCalculatedEvent.Symbol} not found";

            await client.SendAsync("ReceiveMessage", "Mr. Robot", message, quoteCalculatedEvent.RequestDate);

            await args.CompleteMessageAsync(args.Message);
        }
    }
}
