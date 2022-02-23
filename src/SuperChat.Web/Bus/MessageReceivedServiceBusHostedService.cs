using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using SuperChat.Web.Events;
using SuperChat.Web.Models;
using SuperChat.Web.Repositories;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Bus
{
    public class MessageReceivedServiceBusHostedService : AbstractServiceBusHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private const string QUEUE_NAME = "message-received";

        public MessageReceivedServiceBusHostedService(
            ServiceBusClient serviceBusClient,
            IServiceProvider serviceProvider,
            IMapper mapper)
            : base(serviceBusClient, QUEUE_NAME)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public override async Task MessageHandler(ProcessMessageEventArgs args)
        {
            Console.WriteLine("Handle message");

            var messageReceivedEvent = args.Message.Body.ToObjectFromJson<MessageReceivedEvent>();

            using var scope = _serviceProvider.CreateScope();
            var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
            
            var viewModel = _mapper.Map<MessageViewModel>(messageReceivedEvent);
            await messageRepository.Add(viewModel);
            await messageRepository.Commit();

            await args.CompleteMessageAsync(args.Message);
        }
    }
}