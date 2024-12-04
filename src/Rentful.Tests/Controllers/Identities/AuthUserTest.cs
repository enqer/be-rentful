using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using Moq;
using Rentful.Application.UseCases.Commands.AuthUser.Dtos;
using Rentful.Application.UseCases.Commands.LoginUser;
using Rentful.Domain.Options;
using Rentful.Tests.Common.Extensions;
using Rentful.Tests.Common.WebApplicationFactory;
using Rentful.Tests.Controllers.Identities.Factories;
using System.Net;

namespace Rentful.Tests.Controllers.Identities
{
    public class AuthUserTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly Mock<IOptions<JwtSettings>> jwtSettingsMock;

        public AuthUserTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
            this.jwtSettingsMock = new Mock<IOptions<JwtSettings>>();
            ConfigureJwtSettings();
        }
        private void ConfigureJwtSettings()
        {
            var jwtSettings = new JwtSettings
            {
                Key = "Key",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                TimelifeInMinutes = 60
            };

            jwtSettingsMock.Setup(o => o.Value).Returns(jwtSettings);
        }

        [Fact]
        public async Task When_UserDoesntExist_Should_ReturnNotFound()
        {
            // Arrange
            var client = factory.SeedData(dbContext =>
            {
                dbContext.Add(IdentitiesRequestFactory.CreateUser());
            }).CreateClient();
            var itemParameterClient = new IdentitiesClient(client);
            var command = new AuthUserUseCase.Command("test23@wp.pl", "Qwerty123@");

            // Act
            var response = await itemParameterClient.AuthUser(command);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task When_UserWithIncorrectPassword_Should_ReturnBadRequest()
        {
            // Arrange
            var client = factory.SeedData(dbContext =>
            {
                dbContext.Add(IdentitiesRequestFactory.CreateUser());
            }).CreateClient();
            var itemParameterClient = new IdentitiesClient(client);
            var command = new AuthUserUseCase.Command("test@wp.pl", "qweqw3213sdfweQW#E12");

            // Act
            var response = await itemParameterClient.AuthUser(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task When_UserExistWithCorrectPassword_Should_ReturnOKStatusCodeAndAccessToken()
        {
            // Arrange
            var client = factory.SeedData(dbContext =>
            {
                dbContext.Add(IdentitiesRequestFactory.CreateUser());
            }).CreateClient();
            var itemParameterClient = new IdentitiesClient(client);
            var command = new AuthUserUseCase.Command("test@wp.pl", "Qwerty123@");

            // Act
            var response = await itemParameterClient.AuthUser(command);

            // Assert
            var result = await response.ReadNotNullJsonAsync<AuthResponse>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(result.AccessToken);
        }
    }
}
