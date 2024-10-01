using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("telephone_number")]
        public string TelephoneNumber { get; set; } = string.Empty;
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("addressId")]
        public int AddressId { get; set; }
        public Address Address { get; set; } = new Address();

        public List<Apartment> RentedApartments { get; set; } = new List<Apartment>();

        public List<Apartment> OwnedApartments { get; set; } = new List<Apartment>();
    }
}
