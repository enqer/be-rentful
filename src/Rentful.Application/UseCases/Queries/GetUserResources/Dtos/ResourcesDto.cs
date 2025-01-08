namespace Rentful.Application.UseCases.Queries.GetUserResources.Dtos
{
    public class ResourcesDto
    {
        public IEnumerable<AnnouncementDto> Announcements { get; set; } = Enumerable.Empty<AnnouncementDto>();
        public IEnumerable<ApartmentDto> Apartments { get; set; } = Enumerable.Empty<ApartmentDto>();
    }
}
