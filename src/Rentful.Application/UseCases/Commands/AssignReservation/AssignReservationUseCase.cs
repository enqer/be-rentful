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

        internal class Handler(IRepository repository, IUserResolver userResolver, IEmailSender emailSender) : IRequestHandler<Command>
        {

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var reservation = repository.Reservations.FirstOrDefault(x => x.Id == request.Id)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd przypisania rezerwacji", "Nie znaleziono rezerwacji");
                var user = repository.Users.FirstOrDefault(x => x.Id == userResolver.UserId)
                     ?? throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd przypisania rezerwacji", "Nie znaleziono użytkownika");
                var apartment = repository.Announcements.FirstOrDefault(x => x.Id == reservation.AnnouncementId)
                    ?? throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd przypisania rezerwacji", "Nie znaleziono ogłoszenia");
                var location = apartment.Apartment.Location;
                reservation.User = user;
                reservation.Status = ReservationStatusEnum.Unresolved;

                await emailSender.SendEmailAsync(user.Email, "Umówiłeś się na spotkanie!", $"Data: {reservation.Date} \n Adres geograficzny:\n Lat: {location.Latitude}, Lng: {location.Longitude}");
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
