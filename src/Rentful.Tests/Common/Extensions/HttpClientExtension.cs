using Rentful.Domain.Entities;

namespace Rentful.Tests.Common.Extensions
{
    public static class HttpClientExtension
    {
        public static void Auth(this HttpClient httpClient, User? user)
        {
            httpClient.DefaultRequestHeaders.Remove(TestAuthHandler.UserId);
            httpClient.DefaultRequestHeaders.Remove(TestAuthHandler.Role);
            if (user != null)
            {
                httpClient.DefaultRequestHeaders.Add(TestAuthHandler.UserId, user.Id.ToString());
                foreach (var role in user.Roles)
                {
                    httpClient.DefaultRequestHeaders.Add(TestAuthHandler.Role, role);
                }
            }
        }
    }
}
