using Rentful.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("reports", Schema = "rentful")]
    public class Report
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public string Date { get; set; } = string.Empty;
        [Column("type")]
        public ReportTypeEnum Type { get; set; }
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("status")]
        public ReportStatusEnum Status { get; set; }
        [Column("feedback")]
        public string Feedback { get; set; } = string.Empty;
        [Column("lease_agreement_id")]
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; } = new LeaseAgreement();
    }
}
