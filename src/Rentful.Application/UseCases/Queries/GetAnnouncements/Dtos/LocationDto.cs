namespace Rentful.Application.UseCases.Queries.GetAnnouncements.Dtos
{
    public class LocationDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsPrecise { get; set; }
    }
}
