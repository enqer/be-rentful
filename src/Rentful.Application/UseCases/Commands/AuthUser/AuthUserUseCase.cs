using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Commands.AuthUser.Dtos;
using Rentful.Domain.Exceptions;
using Rentful.Domain.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Rentful.Application.UseCases.Commands.LoginUser
{
    public static class AuthUserUseCase
    {
        public record Command(string Email, string Password) : IRequest<AuthResponse>;
        internal class Handler(IOptions<JwtSettings> jwtSettings, IRepository repository) : IRequestHandler<Command, AuthResponse>
        {
            private readonly JwtSettings jwtSettings = jwtSettings.Value;
            private readonly IRepository repository = repository;

            public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = repository
                    .Users
                    .Include(x => x.Roles)
                    .Include(x => x.Announcements)
                    .ThenInclude(x => x.Apartment)
                    .FirstOrDefault(x => x.Email == request.Email)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd użytkownika", "Nie znaleziono użytkownika");
                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (!isPasswordCorrect)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd logowania", "Nie prawidłowe hasło");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

                var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub, "rentful"),
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                };
                user.Roles.ForEach(x => claims.Add(new Claim("roles", x.Name)));
                user.Announcements.Select(x => x.Apartment).ToList().ForEach(x => claims.Add(new Claim("userApartments", x.Id.ToString())));

                var tokenDesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(jwtSettings.TimelifeInMinutes),
                    Issuer = jwtSettings.Issuer,
                    Audience = jwtSettings.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDesc);
                var jwt = new AuthResponse
                {
                    AccessToken = tokenHandler.WriteToken(token)
                };
                return await Task.FromResult(jwt);
            }
        }
    }
}
