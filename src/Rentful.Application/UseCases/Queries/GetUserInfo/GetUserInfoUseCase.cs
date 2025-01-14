using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserInfo.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUserInfo
{
    public class GetUserInfoUseCase
    {
        public record Query : IRequest<UserDto>;

        internal class Handler(IUserResolver userResolver, IRepository repository) : IRequestHandler<Query, UserDto>
        {
            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await repository.Users.FirstAsync(x => x.Id == userResolver.UserId, cancellationToken);
                return new UserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    GlobalId = user.GlobalId
                };
            }
        }
    }
}
