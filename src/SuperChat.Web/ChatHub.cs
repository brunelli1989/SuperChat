using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SuperChat.Domain.Commands;
using SuperChat.Web.Bus;
using SuperChat.Web.Events;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web
{
    public class ChatHub : Hub
    {
        private readonly IServiceProvider _services;

        public ChatHub(IServiceProvider services)
        {
            _services = services;
        }

        public async Task SendMessage(Guid groupId, string message, DateTime actualDate)
        {
            var name = Context.User.Identity.Name;
            var group = Clients.Group(groupId.ToString());
            var connectionId = GetConnectionId();

            using var scope = _services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceBus>();

            if (IsCommand(message))
            {
                if (IsValidCommand(message, out string stockCode))
                {
                    var command = new CalculateQuoteCommand
                    {
                        CorrelationId = connectionId,
                        StockCode = stockCode,
                        RequestDate = actualDate
                    };
                    await service.Send(command);
                }
                else
                {
                    var client = Clients.Client(connectionId);
                    await client.SendAsync("ReceiveMessage", "Mr. Robot", "Invalid command", actualDate);
                }
            }
            else
            {
                await group.SendAsync("ReceiveMessage", name, message, actualDate);
                var messageReceived = new MessageReceivedEvent { UserName = name, Text = message, Date = actualDate, GroupId = groupId };
                await service.Publish(messageReceived);
            }
        }

        private bool IsCommand(string message)
        {
            return message.StartsWith("/");
        }

        private bool IsValidCommand(string message, out string stockCode)
        {
            //TODO: regex?
            if (message.StartsWith("/stock="))
            {
                stockCode = message.Replace("/stock=", string.Empty);
                return true;
            }
            stockCode = null;
            return false;
        }

        public async Task AddToGroup(string groupId, string connectionId)
        {
            await Groups.AddToGroupAsync(connectionId, groupId);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}