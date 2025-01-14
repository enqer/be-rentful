using Rentful.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("lease_agreements", Schema = "rentful")]
    public class LeaseAgreement
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("start_date")]
        public string StartDate { get; set; } = string.Empty;
        [Column("end_date")]
        public string EndDate { get; set; } = string.Empty;
        [Column("status")]
        public LeaseAgreementStatusEnum Status { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();

        public int TenantId { get; set; }
        public User Tenant { get; set; } = new User();
    }
}
