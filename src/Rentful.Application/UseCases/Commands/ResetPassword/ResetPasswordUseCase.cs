using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.RemindPassword
{
    public static class ResetPasswordUseCase
    {
        public record Command(string Email) : IRequest;

        internal class Handler(IEmailSender emailSender, IRepository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = repository.Users.FirstOrDefault(x => x.Email == request.Email)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd", "Nie znaleziono użytkownika");
                var newPassword = GeneratePassword();
                var hashedPassowrd = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.Password = hashedPassowrd;
                await emailSender.SendEmailAsync(request.Email, "Reset hasła", $"Twoje hasło to: {newPassword}\nPamiętaj, aby zmienić hasło w profilu zaraz po zalogowaniu");
            }

            private string GeneratePassword(int length = 8)
            {
                const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
                const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string digits = "0123456789";
                const string specialChars = "!@#$%^&*()-_=+[]{}|;:',.<>?";
                const string allChars = lowerCase + upperCase + digits + specialChars;

                var random = new Random();
                char[] password = new char[length];

                password[0] = lowerCase[random.Next(lowerCase.Length)];
                password[1] = upperCase[random.Next(upperCase.Length)];
                password[2] = digits[random.Next(digits.Length)];
                password[3] = specialChars[random.Next(specialChars.Length)];

                for (int i = 4; i < length; i++)
                {
                    password[i] = allChars[random.Next(allChars.Length)];
                }
                return new string(password.OrderBy(_ => random.Next()).ToArray());
            }
        }
    }
}
