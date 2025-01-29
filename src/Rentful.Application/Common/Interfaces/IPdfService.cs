using Rentful.Application.Pdfs.TenantRaport.Dtos;

namespace Rentful.Application.Common.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateTenantRaportPdf(TenantRaportDto tenantRaport);
    }
}
