using Rentful.Domain.Entities.Enums;

namespace Rentful.Application.UseCases.Queries.GetUserReservations.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public ReservationStatusEnum Status { get; set; }
        public string Date { get; set; } = string.Empty;
        public int AnnouncementId { get; set; }
    }
}
