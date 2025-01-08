namespace Rentful.Application.UseCases.Queries.GetUserResources.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }
}
