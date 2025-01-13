using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("users", Schema = "rentful")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("telephone_number")]
        public string? TelephoneNumber { get; set; } = string.Empty;
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("address_id")]
        public int? AddressId { get; set; }
        public Address Address { get; set; } = new Address();
        public List<Announcement> Announcements { get; set; } = new List<Announcement>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<LeaseAgreement> LeaseAgreements { get; set; } = new List<LeaseAgreement>();

    }
}
