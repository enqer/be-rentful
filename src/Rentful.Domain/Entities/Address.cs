using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    public class Address
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("postal_code")]
        public string PostalCode { get; set; } = string.Empty;
        [Column("building_number")]
        public string BuildingNumber { get; set; } = string.Empty;
        [Column("street")]
        public string Street { get; set; } = string.Empty;
        [Column("city")]
        public string City { get; set; } = string.Empty;
        [Column("country")]
        public string Country { get; set; } = string.Empty;
    }
}
