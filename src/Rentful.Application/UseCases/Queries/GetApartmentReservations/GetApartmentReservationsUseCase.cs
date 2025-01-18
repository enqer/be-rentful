using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetApartmentReservations.Dtos;

namespace Rentful.Application.UseCases.Queries.GetApartmentReservations
{
    public static class GetApartmentReservationsUseCase
    {
        public record Query(int ApartmentId) : IRequest<IEnumerable<ReservationDto>>;

        internal class Handler(IRepository repository) : IRequestHandler<Query, IEnumerable<ReservationDto>>
        {
            public async Task<IEnumerable<ReservationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reservations = await repository
                    .Announcements
                    .Include(x => x.Reservations)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApartmentId == request.ApartmentId)
                    .SelectMany(x => x.Reservations)
                    .ToListAsync(cancellationToken);
                return reservations.Select(x => new ReservationDto
                {
                    Date = x.Date,
                    GlobalId = x.User?.GlobalId,
                    Email = x.User?.Email,
                    FirstName = x.User?.FirstName,
                    LastName = x.User?.LastName,
                    PhoneNumber = x.User?.TelephoneNumber,
                    ReservationId = x.Id,
                    Status = x.Status,
                });
            }
        }
    }
}
