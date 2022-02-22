using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SuperChat.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.Web
{
    public class ServiceBusHostedService : IHostedService, IAsyncDisposable
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ServiceBusProcessor _processor;
        private const string QUEUE_NAME = "quote-calculated";

        public ServiceBusHostedService(ServiceBusClient serviceBusClient, IHubContext<ChatHub> hubContext)
        {
            _serviceBusClient = serviceBusClient;
            _hubContext = hubContext;

            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false
            };
            _processor = serviceBusClient.CreateProcessor(QUEUE_NAME, options);
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
        }

        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            Console.WriteLine("Handle message");

            var quoteCalculatedEvent = args.Message.Body.ToObjectFromJson<QuoteCalculatedEvent>();

            var client = _hubContext.Clients.Client(quoteCalculatedEvent.CorrelationId);

            await client.SendAsync("ReceiveMessage", "Mr. Robot", $"{quoteCalculatedEvent.Symbol} quote is ${quoteCalculatedEvent.High} per share", quoteCalculatedEvent.RequestDate);

            await args.CompleteMessageAsync(args.Message);
        }


        public async Task StartAsync(CancellationToken stoppingToken)
        {
            await _processor.StartProcessingAsync();
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            await _processor.StopProcessingAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _processor.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
