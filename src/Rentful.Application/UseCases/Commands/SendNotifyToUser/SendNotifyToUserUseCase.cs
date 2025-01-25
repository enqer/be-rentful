using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.MassTransit.Models;

namespace Rentful.Application.UseCases.Commands.SendNotifyToUser
{
    public static class SendNotifyToUserUseCase
    {
        public record Command(List<string> Recipients, string Content, string Subject) : IRequest;

        internal class Handler(IMessagePublisher messagePublisher, IUserResolver userResolver) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var notify = new UserNotification
                {
                    Recipients = request.Recipients,
                    Content = request.Content,
                    Subject = request.Subject,
                    SenderEmail = userResolver.Email,
                    SenderFirstName = userResolver.FirstName,
                    SenderLastName = userResolver.LastName
                };
                await messagePublisher.PublishNotificationAsync(notify, cancellationToken);
            }
        }
    }
}
