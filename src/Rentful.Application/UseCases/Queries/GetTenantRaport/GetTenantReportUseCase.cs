using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.Converters;
using Rentful.Application.Pdfs.TenantRaport.Dtos;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Queries.GetTenantRaport
{
    public static class GetTenantReportUseCase
    {
        public record Query(int AgreementId) : IRequest<byte[]>;

        internal class Handler(IRepository repository, IPdfService pdfService) : IRequestHandler<Query, byte[]>
        {
            public async Task<byte[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var agreement = repository.LeaseAgreements
                    .Include(x => x.Tenant)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.Payments)
                    .Include(x => x.Reports)
                    .FirstOrDefault(x => x.Id == request.AgreementId)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd generowania pdf'a", "Nie znaleziono umowy");

                var raportDto = new TenantRaportDto
                {
                    Balance = agreement.Balance,
                    BuildingNumber = agreement.Tenant.Address.BuildingNumber,
                    City = agreement.Tenant.Address.City,
                    Country = agreement.Tenant.Address.Country,
                    PostalCode = agreement.Tenant.Address.PostalCode,
                    Street = agreement.Tenant.Address.Street,
                    Deposit = agreement.Deposit ?? 0,
                    Price = agreement.Price,
                    Rent = agreement.Rent ?? 0,
                    Email = agreement.Tenant.Email,
                    FirstName = agreement.Tenant.FirstName,
                    LastName = agreement.Tenant.LastName,
                    PhoneNumber = agreement.Tenant.TelephoneNumber ?? "---",
                    StartDate = agreement.StartDate,
                    EndDate = agreement.EndDate,
                    Payments = agreement.Payments.ConvertAll(x => new PaymentDto
                    {
                        Amount = x.Amount,
                        Date = x.Date
                    }),
                    Reports = agreement.Reports.ConvertAll(x => new ReportDto
                    {
                        Date = x.Date,
                        Description = x.Description,
                        Feedback = x.Feedback,
                        Status = EnumNameConverter.GetReportStatusName(x.Status),
                        Type = EnumNameConverter.GetReportTypeName(x.Type)
                    }),
                    TenantRating = EnumNameConverter.GetTenantRatingName(agreement.TenantRating),
                    QrCode = "Rentful",
                    PrintDate = DateTime.UtcNow.ToString(),
                };

                var pdf = pdfService.GenerateTenantRaportPdf(raportDto);
                return await Task.FromResult(pdf);
            }
        }
    }
}
