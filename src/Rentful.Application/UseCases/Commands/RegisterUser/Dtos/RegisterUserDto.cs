namespace Rentful.Application.UseCases.Commands.GenerateToken.Dtos
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string MyProperty { get; set; }
    }
}
