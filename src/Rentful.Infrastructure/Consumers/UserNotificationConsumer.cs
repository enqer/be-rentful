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
            Console.WriteLine($"Notification sent to {notification.Email}: {notification.Message}");
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Email, notification.Message);
        }
    }
}
