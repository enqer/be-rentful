namespace Rentful.Application.UseCases.Queries.GetAnnouncements.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        public double Rent { get; set; }
        public double Area { get; set; }
        public short NumberOfRooms { get; set; }
        public bool IsAnimalFriendly { get; set; }
        public bool HasElevator { get; set; }
        public bool IsFurnished { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParkingSpace { get; set; }
        public string Image { get; set; } = string.Empty;
        public LocationDto Location { get; set; } = new LocationDto();
    }
}
