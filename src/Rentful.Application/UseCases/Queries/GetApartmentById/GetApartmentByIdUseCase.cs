using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetApartmentById.Dtos;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Queries.GetApartmentById
{
    public static class GetApartmentByIdUseCase
    {
        public record Query(int ApartmentId) : IRequest<ApartmentDto>;

        internal class Handler(IRepository repository) : IRequestHandler<Query, ApartmentDto>
        {
            public async Task<ApartmentDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var apartment = await repository
                    .Announcements
                    .Include(x => x.Apartment)
                    .FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId, cancellationToken)
                     ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd", "Nie udało się pobrać informacji o mieszkaniu");


                return new ApartmentDto
                {
                    Id = request.ApartmentId,
                    Tenants = apartment.Apartment.LeaseAgreements.ConvertAll(x => new TenantDto
                    {
                        Id = x.Tenant.Id,
                        FirstName = x.Tenant.FirstName,
                        LastName = x.Tenant.LastName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                };
            }
        }
    }
}
