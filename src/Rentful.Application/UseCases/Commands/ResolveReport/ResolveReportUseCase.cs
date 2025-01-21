using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.ResolveReport
{
    public static class ResolveReportUseCase
    {
        public record Command(int ReportId, string Feedback, ReportStatusEnum Status) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var report = repository.Reports.FirstOrDefault(x => x.Id == request.ReportId)
                      ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd", "Nie znaleziono zgłoszenia");
                report.Feedback = request.Feedback;
                report.Status = request.Status;
                repository.SaveChanges();

                await Task.CompletedTask;
            }
        }
    }
}
