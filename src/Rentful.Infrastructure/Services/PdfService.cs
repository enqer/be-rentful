using QuestPDF.Fluent;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.Pdfs.TenantRaport;
using Rentful.Application.Pdfs.TenantRaport.Dtos;

namespace Rentful.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateTenantRaportPdf(TenantRaportDto tenantRaport)
        {
            var raportPdf = new RaportPdf(tenantRaport);
            return raportPdf.GeneratePdf();
        }
    }
}
