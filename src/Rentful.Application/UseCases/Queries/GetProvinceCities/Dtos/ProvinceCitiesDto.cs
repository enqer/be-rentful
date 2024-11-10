namespace Rentful.Application.UseCases.Queries.GetLocationsProvince.Dtos
{
    public class ProvinceCitiesDto
    {
        public string Province { get; set; } = string.Empty;
        public IEnumerable<string> Cities { get; set; } = Enumerable.Empty<string>();
    }
}
