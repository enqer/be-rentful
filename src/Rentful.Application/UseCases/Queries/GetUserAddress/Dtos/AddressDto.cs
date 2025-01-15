namespace Rentful.Application.UseCases.Queries.GetUserAddress.Dtos
{
    public class AddressDto
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
