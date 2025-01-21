using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.ConstantValues;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.SetLeaseAgreementStatus
{
    public static class SetLeaseAgreementStatusUseCase
    {
        public record Command(int AgreementId, LeaseAgreementStatusEnum Status) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var leaseAgreement = await repository.LeaseAgreements.Include(x => x.Tenant).FirstOrDefaultAsync(x => x.Id == request.AgreementId, cancellationToken)
                     ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd zmiany statusu umowy", "Nie znaleziono umowy");
                leaseAgreement.Status = request.Status;
                var role = new Role
                {
                    Type = RoleEnum.Renter,
                    Name = RoleName.Renter
                };
                leaseAgreement.Tenant.Roles.Add(role);
                repository.SaveChanges();
            }
        }
    }
}
