using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("locations", Schema = "rentful")]
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("latitude")]
        public decimal Latitude { get; set; }

        [Column("longitude")]
        public decimal Longitude { get; set; }

        [Column("province")]
        public string Province { get; set; } = string.Empty;

        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Column("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        [Column("is_precise")]
        public bool IsPrecise { get; set; }

    }
}
