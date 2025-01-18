namespace Rentful.Application.UseCases.Queries.GetApartmentById.Dtos
{
    public class TenantDto
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public short Rating { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
