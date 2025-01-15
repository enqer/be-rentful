using Rentful.Domain.Entities.Enums;

namespace Rentful.Application.UseCases.Queries.GetUserLeaseAgreements.Dtos
{
    public class LeaseAgreementDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public LeaseAgreementStatusEnum Status { get; set; }
        public double Price { get; set; }
        public double? Rent { get; set; }
        public double? Deposit { get; set; }
    }
}
