namespace Rentful.Application.Common.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishNotificationAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}
