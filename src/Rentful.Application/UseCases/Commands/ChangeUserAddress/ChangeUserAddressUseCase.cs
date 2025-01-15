using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;

namespace Rentful.Application.UseCases.Commands.ChangeUserAddress
{
    public static class ChangeUserAddressUseCase
    {
        public record Command(string PostalCode, string Street, string City, string BuildingNumber, string Country) : IRequest;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await repository
                    .Users
                    .Include(x => x.Address)
                    .FirstAsync(x => x.Id == userResolver.UserId, cancellationToken);
                if (user.AddressId == null)
                {
                    var address = new Address
                    {
                        PostalCode = request.PostalCode,
                        Street = request.Street,
                        City = request.City,
                        BuildingNumber = request.BuildingNumber,
                        Country = request.Country,
                    };
                    user.Address = address;
                }
                else
                {
                    user.Address.PostalCode = request.PostalCode;
                    user.Address.Street = request.Street;
                    user.Address.City = request.City;
                    user.Address.BuildingNumber = request.BuildingNumber;
                    user.Address.Country = request.Country;
                }

                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
