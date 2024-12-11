namespace Rentful.Application.UseCases.Queries.GetAnnouncementById.Dtos
{
    public class AnnouncementDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        public double Rent { get; set; }
        public double Deposit { get; set; }
        public double Area { get; set; }
        public short NumberOfRooms { get; set; }
        public bool IsAnimalFriendly { get; set; }
        public bool HasElevator { get; set; }
        public bool IsFurnished { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParkingSpace { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? TelephoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsPrecise { get; set; }
        public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
    }
}
