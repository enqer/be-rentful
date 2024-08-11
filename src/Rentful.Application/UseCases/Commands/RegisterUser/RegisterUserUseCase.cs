using MediatR;
using Rentful.Application.UseCases.Commands.GenerateToken.Dtos;

namespace Rentful.Application.UseCases.Commands.RegisterUser
{
    public static class RegisterUserUseCase
    {
        public record Command(RegisterUserDto Register) : IRequest<string>;

        internal class Handler : IRequestHandler<Command, string>
        {


            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {

                return await Task.FromResult("eqwe");
            }
        }
    }
}
