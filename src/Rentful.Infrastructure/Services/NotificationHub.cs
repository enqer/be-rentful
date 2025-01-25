using Microsoft.AspNetCore.SignalR;
using Rentful.Application.Common.Interfaces;

namespace Rentful.Infrastructure.Services
{
    public class NotificationHub(IUserResolver userResolver) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"User connected: {Context.ConnectionId}");
            if (userResolver.Email is not null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userResolver.Email);

            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (userResolver.Email is not null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userResolver.Email);
            }
            Console.WriteLine($"User disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
