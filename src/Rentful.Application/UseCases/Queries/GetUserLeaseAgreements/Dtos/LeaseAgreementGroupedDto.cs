namespace Rentful.Application.UseCases.Queries.GetUserLeaseAgreements.Dtos
{
    public class LeaseAgreementGroupedDto
    {
        public int AnnouncementId { get; set; }
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public List<LeaseAgreementDto> LeaseAgreements { get; set; } = new List<LeaseAgreementDto>();
    }
}
