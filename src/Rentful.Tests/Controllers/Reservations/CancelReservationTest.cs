using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Rentful.Application.Common.Interfaces;
using Rentful.Tests.Common.WebApplicationFactory;
using Rentful.Tests.Controllers.Reservations.Factories;
using System.Net;

namespace Rentful.Tests.Controllers.Reservations
{
    public class CancelReservationTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly Mock<IUserResolver> userResolverMock = new Mock<IUserResolver>();
        public CancelReservationTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task When_ReservationIsMissing_Should_ThrowExceptionAndReturnNotFound()
        {
            // Arrange
            userResolverMock.Setup(x => x.UserId).Returns(21);
            var client = factory
                .SeedData(dbContext =>
                {
                    dbContext.Users.AddRange(ReservationsRequestFactory.CreateUserReservations());
                    dbContext.SaveChanges();
                })
                .Mock(userResolverMock)
                .CreateClient();
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.CancelReservation(100, ReservationsRequestFactory.CreateAuthUser());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_CancelReservation()
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
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.CancelReservation(1, ReservationsRequestFactory.CreateAuthUser());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
