using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.CancelReservationByOwner
{
    public static class CancelReservationByOwnerUseCase
    {
        public record Command(int ReservationId) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var reservation = repository.Reservations.FirstOrDefault(x => x.Id == request.ReservationId)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd odwołania rezerwacji", "Nie można odnaleźć rezerwacji");
                reservation.Status = ReservationStatusEnum.Unavailable;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
