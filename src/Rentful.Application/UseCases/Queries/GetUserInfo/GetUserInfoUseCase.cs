using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserInfo.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUserInfo
{
    public class GetUserInfoUseCase
    {
        public record Query : IRequest<UserDetailsDto>;

        internal class Handler(IUserResolver userResolver, IRepository repository) : IRequestHandler<Query, UserDetailsDto>
        {
            public async Task<UserDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await repository.Users.FirstAsync(x => x.Id == userResolver.UserId, cancellationToken);
                return new UserDetailsDto
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
