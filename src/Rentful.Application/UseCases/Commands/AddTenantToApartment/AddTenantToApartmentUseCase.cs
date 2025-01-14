using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AddTenantToApartment
{
    public static class AddTenantToApartmentUseCase
    {
        public record Command(int ApartmentId, string TenantGlobalId, string StartDate, string EndDate) : IRequest;

        internal class Handler(IRepository repository, IUserResolver userResolver, IEmailSender emailSender) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = repository.Users.FirstOrDefault(x => x.GlobalId.ToString() == request.TenantGlobalId)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd dodania lokatora", "Nie znaleziono użytkownika");
                var apartment = repository.Apartments.First(x => x.Id == request.ApartmentId);
                var agreement = new LeaseAgreement
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IsAccepted = false,
                    Tenant = user
                };
                apartment.LeaseAgreements.Add(agreement);
                //user.LeaseAgreements.Add(agreement);
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
