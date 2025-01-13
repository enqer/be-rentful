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
            public Task<ApartmentDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var apartment = repository
                    .Announcements
                    .Include(x => x.Apartment)
                    .FirstOrDefault(x => x.ApartmentId == request.ApartmentId)
                     ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd", "Nie udało się pobrać informacji o mieszkaniu");


                throw new NotImplementedException();
            }
        }
    }
}
