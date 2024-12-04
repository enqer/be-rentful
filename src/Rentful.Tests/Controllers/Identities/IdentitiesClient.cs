using Rentful.Application.UseCases.Commands.LoginUser;
using Rentful.Tests.Common.Extensions;
using Rentful.Tests.Common.Models;
using System.Net.Http.Json;

namespace Rentful.Tests.Controllers.Identities
{
    public class IdentitiesClient(HttpClient httpClient)
    {

        public async Task<HttpResponseMessage> AuthUser(AuthUserUseCase.Command command, User? user = null)
        {
            httpClient.Auth(user);
            return await httpClient.PostAsJsonAsync("/api/v1/identities/auth", command);
        }
    }
}
