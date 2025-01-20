using Rentful.Domain.Entities.Enums;

namespace Rentful.Application.UseCases.Queries.GetReportsByLeaseAgreement.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public ReportTypeEnum Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public ReportStatusEnum Status { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public int LeaseAgreementId { get; set; }
    }
}
