using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Commands.AuthUser.Dtos;
using Rentful.Domain.Options;
using System.IdentityModel.Tokens.Jwt;
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
                var user = repository.Users.FirstOrDefault(x => x.Email == request.Email);
                if (user == null)
                {
                    throw new Exception();
                }
                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (!isPasswordCorrect)
                {
                    throw new Exception();
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

                var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub, "rentful"),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                };
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
