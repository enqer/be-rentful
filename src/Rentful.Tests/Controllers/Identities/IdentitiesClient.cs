using Rentful.Application.UseCases.Commands.LoginUser;
using System.Net.Http.Json;

namespace Rentful.Tests.Controllers.Identities
{
    public class IdentitiesClient(HttpClient httpClient)
    {

        public async Task<HttpResponseMessage> AuthUser(AuthUserUseCase.Command command)
        {
            return await httpClient.PostAsJsonAsync("/api/v1/identities/auth", command);
        }
    }
}
