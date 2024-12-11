using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AssignReservation
{
    public static class AssignReservationUseCase
    {
        public record Command(int Id) : IRequest;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Command>
        {

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var reservation = repository.Reservations.FirstOrDefault(x => x.Id == request.Id)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd przypisania rezerwacji", "Nie znaleziono rezerwacji");
                var user = repository.Users.First(x => x.Id == userResolver.UserId);
                reservation.User = user;
                reservation.Status = ReservationStatusEnum.Unresolved;
                await repository.SaveChangesAsync();
            }
        }
    }
}
