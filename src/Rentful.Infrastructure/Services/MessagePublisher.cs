using MassTransit;
using Rentful.Application.Common.Interfaces;

namespace Rentful.Infrastructure.Services
{
    public class MessagePublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
    {
        public async Task PublishNotificationAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
