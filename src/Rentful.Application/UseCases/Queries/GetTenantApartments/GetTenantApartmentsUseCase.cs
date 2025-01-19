using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetTenantApartments.Dtos;

namespace Rentful.Application.UseCases.Queries.GetTenantApartments
{
    public class GetTenantApartmentsUseCase
    {
        public record Query() : IRequest<List<ApartmentDto>>;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Query, List<ApartmentDto>>
        {
            public async Task<List<ApartmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var apartmentIds = await repository.LeaseAgreements.Where(x => x.TenantId == userResolver.UserId).Select(x => x.ApartmentId).ToListAsync(cancellationToken);
                var announcements = await repository
                    .Announcements
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.LeaseAgreements)
                    .ThenInclude(x => x.Tenant)
                    .Include(x => x.User)
                    .Where(x => apartmentIds.Contains(x.ApartmentId ?? 0))
                    .ToListAsync(cancellationToken);
                return announcements.ConvertAll(x =>
                {
                    var leaseAgreement = x.Apartment.LeaseAgreements.First(x => x.TenantId == userResolver.UserId);
                    return new ApartmentDto
                    {
                        Id = x.Apartment.Id,
                        Area = x.Apartment.Area,
                        Deposit = x.Apartment.Deposit,
                        HasBalcony = x.Apartment.HasBalcony,
                        HasElevator = x.Apartment.HasElevator,
                        HasParkingSpace = x.Apartment.HasParkingSpace,
                        NumberOfRooms = x.Apartment.NumberOfRooms,
                        IsAnimalFriendly = x.Apartment.IsAnimalFriendly,
                        IsFurnished = x.Apartment.IsFurnished,
                        Price = x.Apartment.Price,
                        Rent = x.Apartment.Rent,
                        EndDate = leaseAgreement.EndDate,
                        StartDate = leaseAgreement.StartDate,
                        LeaseAgreementId = leaseAgreement.Id,
                        OwnerId = x.User.Id,
                        OwnerFirstName = x.User.FirstName,
                        OwnerLastName = x.User.LastName,
                        OwnerEmail = x.User.Email,
                        OwnerPhoneNumber = x.User.TelephoneNumber
                    };
                }
               );
            }
        }
    }
}
