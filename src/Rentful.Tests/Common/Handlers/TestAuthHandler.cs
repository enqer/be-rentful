using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Rentful.Tests.Common.Handlers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public static string AuthenticationScheme = "TestScheme";
        public static string UserId = "UserId";
        public static string Role = "UserId";

        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Test user") };
            var user = GetUserId();
            if (string.IsNullOrWhiteSpace(user))
            {
                return Task.FromResult(AuthenticateResult.Fail("401"));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user));
            claims.AddRange(GetRoles());
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }

        private string? GetUserId()
        {
            if (Context.Request.Headers.TryGetValue(UserId, out var userId))
            {
                return userId[0];
            }
            return null;
        }

        private IEnumerable<Claim> GetRoles()
        {
            if (Context.Request.Headers.TryGetValue(Role, out var roles))
            {
                foreach (var role in roles)
                {
                    yield return new Claim(ClaimTypes.Role, role ?? string.Empty);
                }
            };
        }
    }
}
