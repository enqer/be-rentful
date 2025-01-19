using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.AddReport
{
    public static class AddReportUseCase
    {
        public record Command(ReportTypeEnum Type, string Description, int AgreementId) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var report = new Report
                {
                    Description = request.Description,
                    Date = DateTime.UtcNow.ToString(),
                    Type = request.Type,
                    Status = ReportStatusEnum.Unresolved
                };
                var leaseAgreement = await repository.LeaseAgreements.FirstOrDefaultAsync(x => x.Id == request.AgreementId, cancellationToken)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd dodania zagłoszenia", "Nie można odnaleźć umowy");
                leaseAgreement.Reports.Add(report);
                repository.SaveChanges();
            }
        }
    }
}
