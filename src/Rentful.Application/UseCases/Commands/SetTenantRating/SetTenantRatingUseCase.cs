using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.SetTenantRating
{
    public static class SetTenantRatingUseCase
    {
        public record Command(int AgreementId, TenantRatingEnum Rate) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var agreement = repository.LeaseAgreements.FirstOrDefault(x => x.Id == request.AgreementId)
                     ?? throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd oceniania lokatora", "Nie znaleziono umowy");
                agreement.TenantRating = request.Rate;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
