using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AddTenantToApartment
{
    public static class AddTenantToApartmentUseCase
    {
        public record Command(int ApartmentId, string TenantGlobalId, string StartDate, string EndDate, int Price, int Rent, int Deposit) : IRequest;

        internal class Handler(IRepository repository, IEmailSender emailSender) : IRequestHandler<Command>
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
                    Status = LeaseAgreementStatusEnum.Unresolved,
                    Tenant = user,
                    Deposit = request.Deposit,
                    Price = request.Price,
                    Rent = request.Rent
                };
                apartment.LeaseAgreements.Add(agreement);
                await repository.SaveChangesAsync(cancellationToken);
                await emailSender.SendEmailAsync(user.Email, "Otrzymałeś umowę najmu do zaakceptowania!", "Możesz ją sprawdzić w swoim profilu a następnie zdecydować czy chcesz ją przyjąć czy też odrzucić");
            }
        }
    }
}
