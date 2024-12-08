using Rentful.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("reservations", Schema = "rentful")]
    public class Reservation
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public string Date { get; set; } = string.Empty;
        [Column("time")]
        public string Time { get; set; } = string.Empty;
        [Column("status")]
        public ReservationStatusEnum Status { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; } = new User();

        [Column("announcement_id")]
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; } = new Announcement();

    }
}
