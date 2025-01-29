namespace Rentful.Application.Pdfs.TenantRaport.Dtos
{
    public class TenantRaportDto
    {
        public string QrCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string TenantRating { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Rent { get; set; }
        public double Deposit { get; set; }
        public double Balance { get; set; }
        public string PrintDate { get; set; } = string.Empty;

        public List<ReportDto> Reports { get; set; } = new List<ReportDto>();
        public List<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    }
}
