using System.Net;
using System.Net.Http.Json;

namespace Rentful.Tests.Common.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ReadNotNullJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var result = await httpResponseMessage.Content.ReadFromJsonAsync<T>();
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.NotNull(result);
            return result;
        }
    }
}
