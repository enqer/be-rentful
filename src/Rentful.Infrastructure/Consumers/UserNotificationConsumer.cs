using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Rentful.Infrastructure.Services;
using Rentful.MassTransit.Models;

namespace Rentful.Infrastructure.Consumers
{
    public class UserNotificationConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<UserNotification>
    {
        public async Task Consume(ConsumeContext<UserNotification> context)
        {
            var notification = context.Message;
            foreach (var email in notification.Recepients)
            {
                await hubContext.Clients.Group(email).SendAsync("user-notifications", notification.Message);
            }
        }
    }
}
