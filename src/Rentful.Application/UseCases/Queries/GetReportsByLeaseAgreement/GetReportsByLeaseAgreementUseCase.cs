using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetReportsByLeaseAgreement.Dtos;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Queries.GetReportsByLeaseAgreement
{
    public static class GetReportsByLeaseAgreementUseCase
    {
        public record Query(int AgreementId) : IRequest<List<ReportDto>>;

        internal class Handler(IRepository repository) : IRequestHandler<Query, List<ReportDto>>
        {
            public async Task<List<ReportDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var agreement = await repository
                    .LeaseAgreements
                    .Include(x => x.Reports)
                    .FirstOrDefaultAsync(x => x.Id == request.AgreementId, cancellationToken)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd podczas pobierania zgłoszeń", "Nie znaleziono zgłoszeń");
                return agreement.Reports.ConvertAll(x => new ReportDto
                {
                    Date = x.Date,
                    Description = x.Description,
                    Feedback = x.Feedback,
                    Id = x.Id,
                    Status = x.Status,
                    Type = x.Type,
                    LeaseAgreementId = request.AgreementId
                });
            }
        }
    }
}
