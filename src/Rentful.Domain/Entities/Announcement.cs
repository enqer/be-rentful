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
        [Column("date_added", TypeName = "timestamp without time zone")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        [Column("date_deleted", TypeName = "timestamp without time zone")]
        public DateTime? DateDeleted { get; set; } = DateTime.UtcNow;

        [Column("apartment_id")]
        public int? ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = new Apartment();

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; } = new User();

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
