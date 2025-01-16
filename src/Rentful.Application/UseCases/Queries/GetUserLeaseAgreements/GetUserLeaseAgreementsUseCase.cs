using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserLeaseAgreements.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUseLeaseAgreements
{
    public static class GetUserLeaseAgreementsUseCase
    {
        public record Query : IRequest<List<LeaseAgreementGroupedDto>>;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Query, List<LeaseAgreementGroupedDto>>
        {
            public async Task<List<LeaseAgreementGroupedDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leaseAgreements = await repository
                    .Announcements
                    .Include(x => x.User)
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.LeaseAgreements)
                    .Where(x => x.Apartment.LeaseAgreements.Select(y => y.TenantId).Contains(userResolver.UserId)).ToListAsync(cancellationToken);

                return leaseAgreements.GroupBy(x => new
                {
                    x.Id,
                    x.User.FirstName,
                    x.User.LastName,
                },
                (key, groups) => new LeaseAgreementGroupedDto
                {
                    AnnouncementId = key.Id,
                    OwnerFirstName = key.FirstName,
                    OwnerLastName = key.LastName,
                    LeaseAgreements = groups.SelectMany(y => y.Apartment.LeaseAgreements.ConvertAll(z => new LeaseAgreementDto
                    {
                        Deposit = z.Deposit ?? 0,
                        EndDate = z.EndDate,
                        Id = z.Id,
                        Price = z.Price,
                        Rent = z.Rent ?? 0,
                        StartDate = z.StartDate,
                        Status = z.Status
                    })).ToList()
                }).ToList();

            }
        }
    }
}
