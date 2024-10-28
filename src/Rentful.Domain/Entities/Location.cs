using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("locations", Schema = "public")]
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("latitude")]
        public decimal Latitude { get; set; }

        [Column("longitude")]
        public decimal Longitude { get; set; }

        [Column("place")]
        public string? Place { get; set; } = string.Empty;

    }
}
