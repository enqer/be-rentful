using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.SendMailToUser
{
    public static class SendMailToUserUseCase
    {
        public record Command(string Subject, string Content, string Recipient) : IRequest;

        internal class Handler(IUserResolver userResolver, IEmailSender emailSender, IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await repository.Users.FirstOrDefaultAsync(x => x.Id == userResolver.UserId, cancellationToken)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd", "Nie znaleziono użytkownika");
                await emailSender.SendEmailFromUserAsync(user, request.Recipient, request.Subject, request.Content);
            }
        }
    }
}
