using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AddReservations
{
    public static class AddReservationsUseCase
    {
        public record Command(int ApartmentId, List<string> Reservations) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var announcement = await repository.Announcements.FirstOrDefaultAsync(x => x.ApartmentId == request.ApartmentId, cancellationToken)
                    ?? throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd przypisania rezerwacji", "Nie znaleziono ogłoszenia");
                var reservations = request.Reservations.ConvertAll(x => new Reservation
                {
                    Date = x,
                    Status = ReservationStatusEnum.Available
                });
                announcement.Reservations.AddRange(reservations);
                repository.SaveChanges();
            }
        }
    }
}
