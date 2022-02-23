using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public abstract class AbstractServiceBusHostedService : IHostedService, IAsyncDisposable
    {
        protected readonly ServiceBusClient _serviceBusClient;
        protected readonly ServiceBusProcessor _processor;

        public AbstractServiceBusHostedService(ServiceBusClient serviceBusClient, string queueName)
        {
            _serviceBusClient = serviceBusClient;

            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false
            };
            _processor = serviceBusClient.CreateProcessor(queueName, options);
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
        }

        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

        public abstract Task MessageHandler(ProcessMessageEventArgs args);


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
