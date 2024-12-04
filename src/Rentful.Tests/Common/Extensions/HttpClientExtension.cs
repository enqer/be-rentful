using Rentful.Tests.Common.Handlers;
using Rentful.Tests.Common.Models;

namespace Rentful.Tests.Common.Extensions
{
    public static class HttpClientExtension
    {
        public static void Auth(this HttpClient httpClient, User? user)
        {
            httpClient.DefaultRequestHeaders.Remove(TestAuthHandler.UserId);
            if (user != null)
            {
                httpClient.DefaultRequestHeaders.Add(TestAuthHandler.UserId, user.Id.ToString());
            }
        }
    }
}
