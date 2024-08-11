using FluentValidation;
using Rentful.Application.UseCases.Commands.GenerateToken.Dtos;

namespace Rentful.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        private const string EmailPattern = @"^[\w\-.]+@([\w-]+\.)+[a-zA-Z]{2,}$";
        private const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}:<>?\-=[\]\\;',./`~])[A-Za-z\d!@#$%^&*()_+{}:<>?\-=[\]\\;',./`~]{8,}$";
        private const string PhoneNumberPattern = @"^\d{9}$";
        private const string ZipCodePattern = @"^\d{5}$";

        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("First name is required.");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Last name is required.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(EmailPattern)
                .WithMessage("Invalid email format");
            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Birth date is required.");
            RuleFor(x => x.Password)
                .Matches(PasswordPattern)
                .WithMessage("Password should be strong");
            RuleFor(x => x.Nationality)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Nationality is required.");
            RuleFor(x => x.PhoneNumber)
                .Matches(PhoneNumberPattern)
                .WithMessage("Phone number should contain exactly 9 digits.");
            RuleFor(x => x.ResidentialAddress.ZipCode)
               .Matches(ZipCodePattern)
               .WithMessage("Zip code must contain 5 digits");
            RuleFor(x => x.ResidentialAddress.City)
                .NotEmpty()
                .WithMessage("City is required.");
            RuleFor(x => x.ResidentialAddress.Country)
                .NotEmpty()
                .WithMessage("Country is required.");
            RuleFor(x => x.ResidentialAddress.Street)
                .NotEmpty()
                .WithMessage("Street is required.");
            RuleFor(x => x.ResidentialAddress.BuildingNumber)
                .NotEmpty()
                .WithMessage("Building number is required.");
            RuleFor(x => x.ResidentialAddress.ApartmentNumber)
                .NotEmpty()
                .WithMessage("Apartment number is required.");
        }
    }
}
