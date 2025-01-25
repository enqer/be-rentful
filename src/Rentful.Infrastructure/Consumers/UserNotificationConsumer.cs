using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Rentful.Infrastructure.Consumers.Dtos;
using Rentful.Infrastructure.Services;
using Rentful.MassTransit.Models;

namespace Rentful.Infrastructure.Consumers
{
    public class UserNotificationConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<UserNotification>
    {
        public async Task Consume(ConsumeContext<UserNotification> context)
        {
            var notification = context.Message;
            var notify = new SendNotify
            {
                SenderLastName = notification.SenderLastName,
                SenderFirstName = notification.SenderFirstName,
                SenderEmail = notification.SenderEmail,
                Subject = notification.Subject,
                Content = notification.Content,
                Guid = Guid.NewGuid(),
            };
            foreach (var email in notification.Recipients)
            {
                await hubContext.Clients.Group(email).SendAsync("user-notifications", notify);
            }
        }
    }
}
