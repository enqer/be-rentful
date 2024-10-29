using FluentValidation;
using Rentful.Application.UseCases.Commands.GenerateToken.Dtos;
using Rentful.Domain.ConstantValues;

namespace Rentful.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {

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
                .Matches(Regex.EmailPattern)
                .WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .Matches(Regex.PasswordPattern)
                .WithMessage("Password should be strong");

        }
    }
}
