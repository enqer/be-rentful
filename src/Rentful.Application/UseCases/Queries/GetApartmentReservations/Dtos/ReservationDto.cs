using Rentful.Domain.Entities.Enums;

namespace Rentful.Application.UseCases.Queries.GetApartmentReservations.Dtos
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public ReservationStatusEnum Status { get; set; }
        public string Date { get; set; } = string.Empty;
        public Guid? GlobalId { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
