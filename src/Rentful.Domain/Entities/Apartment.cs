using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("apartments", Schema = "rentful")]
    public class Apartment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("rent")]
        public double? Rent { get; set; }

        [Column("deposit")]
        public double? Deposit { get; set; }

        [Column("area")]
        public double Area { get; set; }

        [Column("number_of_rooms")]
        public short NumberOfRooms { get; set; }

        [Column("is_furnished")]
        public bool IsFurnished { get; set; }

        [Column("is_animal_friendly")]
        public bool IsAnimalFriendly { get; set; }

        [Column("has_elevator")]
        public bool HasElevator { get; set; }

        [Column("has_balcony")]
        public bool HasBalcony { get; set; }

        [Column("has_parking_space")]
        public bool HasParkingSpace { get; set; }

        [Column("location_id")]
        public int LocationId { get; set; }
        public Location Location { get; set; } = new Location();

        public List<Image> Images { get; set; } = new List<Image>();

        public List<LeaseAgreement> LeaseAgreements { get; set; } = new List<LeaseAgreement>();
    }
}
