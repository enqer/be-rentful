using Microsoft.AspNetCore.Http;
using Rentful.Application.Common.Interfaces;
using System.Security.Claims;

namespace Rentful.Infrastructure.Services
{
    public class UserResolver : IUserResolver
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        public UserResolver(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }
            claimsPrincipal = httpContextAccessor.HttpContext.User;

        }
        public int UserId => Int32.Parse(claimsPrincipal?.FindFirst("userId")?.Value ?? throw new NullReferenceException(nameof(UserId)));
        public string FirstName => claimsPrincipal?.FindFirst("firstName")?.Value ?? "";
        public string LastName => claimsPrincipal?.FindFirst("lastName")?.Value ?? "";
    }
}
