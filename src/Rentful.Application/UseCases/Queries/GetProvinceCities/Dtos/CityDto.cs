namespace Rentful.Application.UseCases.Queries.GetProvinceCities.Dtos
{
    public class CityDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
