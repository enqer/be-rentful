using Rentful.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("lease_agreements", Schema = "rentful")]
    public class LeaseAgreement
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("rating_tenant")]
        public short TenantRating { get; set; }
        [Column("start_date")]
        public string StartDate { get; set; } = string.Empty;
        [Column("end_date")]
        public string EndDate { get; set; } = string.Empty;
        [Column("status")]
        public LeaseAgreementStatusEnum Status { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("rent")]
        public double? Rent { get; set; }
        [Column("deposit")]
        public double? Deposit { get; set; }
        [Column("apartment_id")]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public User Tenant { get; set; } = new User();
    }
}
