using Rentful.Application.UseCases.Commands.RegisterUser.Dtos;

namespace Rentful.Application.UseCases.Commands.GenerateToken.Dtos
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public RegisterAddressDto ResidentialAddress { get; set; } = new RegisterAddressDto();
        public RegisterAddressDto MailingAddress { get; set; } = new RegisterAddressDto();

    }
}
