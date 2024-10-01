using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("apartments", Schema = "public")]
    public class Apartment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("addressId")]
        public int AddressId { get; set; }
        public Address Address { get; set; } = new Address();

        [Column("apartmentDetailsId")]
        public int ApartmentDetailsId { get; set; }
        public ApartmentDetails ApartmentDetails { get; set; } = new ApartmentDetails();

        public int? OwnerId { get; set; }
        public User? Owner { get; set; }

        public int? TenantId { get; set; }
        public User? Tenant { get; set; }
    }
}
