using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserAddress.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUserAddress
{
    public static class GetUserAddressUseCase
    {
        public record Query : IRequest<AddressDto>;

        internal class Handler(IUserResolver userResolver, IRepository repository) : IRequestHandler<Query, AddressDto>
        {
            public async Task<AddressDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await repository
                    .Users
                    .Include(u => u.Address)
                    .FirstAsync(x => x.Id == userResolver.UserId, cancellationToken);
                return new AddressDto
                {
                    Street = user.Address.Street,
                    PostalCode = user.Address.PostalCode,
                    BuildingNumber = user.Address.BuildingNumber,
                    City = user.Address.City,
                    Country = user.Address.Country
                };
            }
        }
    }
}
