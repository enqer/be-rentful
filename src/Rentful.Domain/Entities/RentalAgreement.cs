using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("rental_agreements", Schema = "public")]
    public class RentalAgreement
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("rent_amount")]
        public decimal RentAmount { get; set; }

        [Column("deposit_amount")]
        public decimal DepositAmount { get; set; }

        [Column("agreement_terms")]
        public string AgreementTerms { get; set; } = string.Empty;

        [Column("tenant_id")]
        public int TenantId { get; set; }
        public User Tenant { get; set; } = null!;

        [Column("apartment_id")]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;
    }
}
