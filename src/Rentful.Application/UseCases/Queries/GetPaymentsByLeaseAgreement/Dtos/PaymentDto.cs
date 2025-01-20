namespace Rentful.Application.UseCases.Queries.GetPaymentsByLeaseAgreement.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}
