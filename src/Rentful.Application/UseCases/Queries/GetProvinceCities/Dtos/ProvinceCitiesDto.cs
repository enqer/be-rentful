using Rentful.Application.UseCases.Queries.GetProvinceCities.Dtos;

namespace Rentful.Application.UseCases.Queries.GetLocationsProvince.Dtos
{
    public class ProvinceCitiesDto
    {
        public string Province { get; set; } = string.Empty;
        public IEnumerable<CityDto> Cities { get; set; } = Enumerable.Empty<CityDto>();
    }
}
