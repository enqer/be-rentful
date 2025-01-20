using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("payments", Schema = "rentful")]
    public class Payment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("date")]
        public string Date { get; set; } = string.Empty;
        [Column("lease_agreement_id")]
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; } = new LeaseAgreement();
    }
}
