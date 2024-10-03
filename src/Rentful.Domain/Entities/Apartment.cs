using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("apartments", Schema = "public")]
    public class Apartment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("address_id")]
        public int AddressId { get; set; }
        public Address Address { get; set; } = new Address();

        [Column("apartment_details_id")]
        public int ApartmentDetailsId { get; set; }
        public ApartmentDetails ApartmentDetails { get; set; } = new ApartmentDetails();

        [Column("owner_id")]
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }

        public List<RentalAgreement>? RentalAgreements { get; set; } = new List<RentalAgreement>();
    }
}
