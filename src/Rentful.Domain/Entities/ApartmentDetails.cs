using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("apartment_details", Schema = "public")]
    public class ApartmentDetails
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("area_in_square_meters")]
        public double AreaInSquareMeters { get; set; }

        [Column("number_of_rooms")]
        public short NumberOfRooms { get; set; }

        [Column("number_of_bathrooms")]
        public short NumberOfBathrooms { get; set; }

        [Column("floor")]
        public short Floor { get; set; }

        [Column("year_built")]
        public short YearBuilt { get; set; }

        [Column("has_elevator")]
        public bool HasElevator { get; set; }

        [Column("has_balcony")]
        public bool HasBalcony { get; set; }

        [Column("is_furnished")]
        public bool IsFurnished { get; set; }

        [Column("has_parking_space")]
        public bool HasParkingSpace { get; set; }

        public Apartment Apartment { get; set; } = null!;
    }
}
