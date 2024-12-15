using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.CancelReservation
{
    public static class CancelReservationUseCase
    {
        public record Command(int ReservationId) : IRequest;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var reservation = repository.Reservations.FirstOrDefault(x => x.Id == request.ReservationId && x.UserId == userResolver.UserId)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd odwołania rezerwacji", "Nie można odnaleźć rezerwacji");
                reservation.User = null;
                reservation.Status = ReservationStatusEnum.Available;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
