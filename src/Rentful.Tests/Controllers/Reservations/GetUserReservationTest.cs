using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserReservations.Dtos;
using Rentful.Domain.Entities.Enums;
using Rentful.Tests.Common.Extensions;
using Rentful.Tests.Common.WebApplicationFactory;
using Rentful.Tests.Controllers.Reservations.Factories;

namespace Rentful.Tests.Controllers.Reservations
{
    public class GetUserReservationTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly Mock<IUserResolver> userResolverMock = new Mock<IUserResolver>();
        public GetUserReservationTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Should_ReturnUserReservations()
        {
            // Arrange
            userResolverMock.Setup(x => x.UserId).Returns(22);
            var client = factory
                .SeedData(dbContext =>
                {
                    dbContext.Users.AddRange(ReservationsRequestFactory.CreateUserReservations());
                    dbContext.SaveChanges();
                })
                .Mock(userResolverMock)
                .CreateClient();
            var itemParameterClient = new ReservationsClient(client);

            // Act
            var response = await itemParameterClient.GetUserReservations(ReservationsRequestFactory.CreateAuthUser());

            // Assert
            var result = await response.ReadNotNullJsonAsync<List<ReservationDto>>();
            var reservation = Assert.Single(result);
            Assert.Equal(ReservationStatusEnum.Unresolved, reservation.Status);
            Assert.Equal("2024-12-12 12:12", reservation.Date);
        }
    }
}
