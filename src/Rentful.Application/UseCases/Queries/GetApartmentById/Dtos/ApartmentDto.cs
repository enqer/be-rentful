namespace Rentful.Application.UseCases.Queries.GetApartmentById.Dtos
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public bool HasAnnouncement { get; set; }
        public List<TenantDto> Tenants { get; set; } = new List<TenantDto>();
    }
}
