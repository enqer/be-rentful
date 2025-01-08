namespace Rentful.Application.UseCases.Queries.GetUserResources.Dtos
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public short NumberOfRooms { get; set; }
        public bool IsFurnished { get; set; }
        public bool IsAnimalFriendly { get; set; }
        public bool HasElevator { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParkingSpace { get; set; }
        public string Thumbnail { get; set; } = string.Empty;

    }
}
