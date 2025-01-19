namespace Rentful.Application.UseCases.Queries.GetTenantApartments.Dtos
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public double Price { get; set; }
        public double? Rent { get; set; }
        public double? Deposit { get; set; }
        public double Area { get; set; }
        public short NumberOfRooms { get; set; }
        public bool IsFurnished { get; set; }
        public bool IsAnimalFriendly { get; set; }
        public bool HasElevator { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParkingSpace { get; set; }
        public int OwnerId { get; set; }
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string? OwnerPhoneNumber { get; set; } = string.Empty;
    }
}
