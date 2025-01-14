using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;

namespace Rentful.Application.UseCases.Commands.ChangePassword
{
    public static class ChangePasswordUseCase
    {
        public record Command(string Password) : IRequest;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await repository.Users.FirstAsync(x => x.Id == userResolver.UserId, cancellationToken);
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Password = hashedPassword;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
