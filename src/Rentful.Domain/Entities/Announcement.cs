using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("announcements", Schema = "rentful")]
    public class Announcement
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("date_added")]
        public string DateAdded { get; set; } = string.Empty;

        [Column("apartment_id")]
        public int? ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();
    }
}
