using Microsoft.AspNetCore.Mvc.Testing;
using Rentful.Application.UseCases.Commands.RegisterUser;
using Rentful.Tests.Common.WebApplicationFactory;
using Rentful.Tests.Controllers.Identities.Factories;
using System.Net;

namespace Rentful.Tests.Controllers.Identities
{
    public class RegisterUserTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public RegisterUserTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
        }


        [Fact]
        public async Task When_UserAlreadyExist_Should_ReturnBadRequest()
        {
            // Arrange
            var client = factory.SeedData(dbContext =>
            {
                dbContext.Add(IdentitiesRequestFactory.CreateUser());
            }).CreateClient();
            var itemParameterClient = new IdentitiesClient(client);
            var command = new RegisterUserUseCase.Command("Rick", "Sorkin", "test@wp.pl", "Qwerty123@");

            // Act
            var response = await itemParameterClient.RegisterUser(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task When_UserDoesntExist_Should_RegisterUserAndReturnOkStatusCode()
        {
            // Arrange
            var client = factory.CreateClient();
            var itemParameterClient = new IdentitiesClient(client);
            var command = new RegisterUserUseCase.Command("Rick", "Sorkin", "test213@wp.pl", "Qwerty123@");

            // Act
            var response = await itemParameterClient.RegisterUser(command);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
