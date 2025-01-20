using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AddPayment
{
    public static class AddPaymentUseCase
    {
        public record Command(int AgreementId) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var leaseAgreement = repository.LeaseAgreements.FirstOrDefault(x => x.Id == request.AgreementId)
                     ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd dodania płatności", "Nie można odnaleźć umowy");

                var newPayment = new Payment
                {
                    Date = DateTime.UtcNow.ToString(),
                    Amount = leaseAgreement.Price + leaseAgreement.Rent ?? 0,
                };
                leaseAgreement.Balance += newPayment.Amount;
                leaseAgreement.Payments.Add(newPayment);
                repository.SaveChanges();

                await Task.CompletedTask;
            }
        }
    }
}
