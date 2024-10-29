using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("images", Schema = "rentful")]
    public class Image
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("is_thumbnail")]
        public bool IsThumbnail { get; set; }
        [Column("source")]
        public string Source { get; set; } = string.Empty;
        [Column("apartment_id")]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();

    }
}
