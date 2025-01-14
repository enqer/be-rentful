namespace Rentful.Application.UseCases.Queries.GetUserInfo.Dtos
{
    public class UserDto
    {
        public Guid GlobalId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
