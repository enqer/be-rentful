using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetPaymentsByLeaseAgreement.Dtos;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Queries.GetPaymentsByLeaseAgreement
{
    public static class GetPaymentsByLeaseAgreementUseCase
    {
        public record Query(int AgreementId) : IRequest<List<PaymentDto>>;

        internal class Handler(IRepository repository) : IRequestHandler<Query, List<PaymentDto>>
        {
            public async Task<List<PaymentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var agreement = await repository
                   .LeaseAgreements
                   .Include(x => x.Payments)
                   .FirstOrDefaultAsync(x => x.Id == request.AgreementId, cancellationToken)
                   ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd podczas pobierania zgłoszeń", "Nie znaleziono zgłoszeń");

                var amountToPay = agreement.Price + agreement.Rent ?? 0;
                return agreement.Payments.ConvertAll(x => new PaymentDto
                {
                    Date = x.Date,
                    Id = x.Id,
                    Amount = amountToPay
                });
            }
        }
    }
}
