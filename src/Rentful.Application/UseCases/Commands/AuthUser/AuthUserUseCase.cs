using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentful.Domain.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rentful.Application.UseCases.Commands.LoginUser
{
    public static class AuthUserUseCase
    {
        public record Command(string Email, string Password) : IRequest<string>;
        internal class Handler : IRequestHandler<Command, string>
        {
            private readonly JwtSettings jwtSettings;
            public Handler(IOptions<JwtSettings> jwtSettings)
            {
                this.jwtSettings = jwtSettings.Value;
            }
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                // Checking in db 
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "test@wp.pl"),
                new Claim(JwtRegisteredClaimNames.Email, "test@wp.pl"),
                new Claim("TestowyClaim", "value")
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
                var jwt = tokenHandler.WriteToken(token);
                return await Task.FromResult(jwt);
            }
        }
    }
}
