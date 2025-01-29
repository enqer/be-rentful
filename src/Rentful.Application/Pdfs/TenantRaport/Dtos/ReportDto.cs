namespace Rentful.Application.Pdfs.TenantRaport.Dtos
{
    public class ReportDto
    {
        public string Date { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Feedback { get; set; } = string.Empty;
    }
}
