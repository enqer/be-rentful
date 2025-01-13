using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("lease_agreements", Schema = "rentful")]
    public class LeaseAgreement
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();

        public int TenantId { get; set; }
        public User Tenant { get; set; } = new User();
    }
}
