using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperChat.Domain.Commands;
using SuperChat.Domain.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.WebApi
{
    public class ServiceBusHostedService : IHostedService, IAsyncDisposable
    {
        private readonly IServiceProvider _services;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _processor;
        private const string QUEUE_NAME = "calculate-quote";

        public ServiceBusHostedService(ServiceBusClient serviceBusClient, IServiceProvider services)
        {
            _services = services;
            _serviceBusClient = serviceBusClient;
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
            using var scope = _services.CreateScope();

            var calculateQuoteCommand = args.Message.Body.ToObjectFromJson<CalculateQuoteCommand>();

            var quoteCalculator = scope.ServiceProvider.GetRequiredService<IQuoteCalculator>();

            await quoteCalculator.CalculateQuote(calculateQuoteCommand);

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
