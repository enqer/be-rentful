using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Rentful.Application.Common.Interfaces;
using Rentful.Tests.Common.WebApplicationFactory;
using Rentful.Tests.Controllers.Reservations.Factories;
using System.Net;

namespace Rentful.Tests.Controllers.Reservations
{
    public class AssignReservationTest : IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly Mock<IUserResolver> userResolverMock = new Mock<IUserResolver>();
        public AssignReservationTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task When_UserNotLogged_Should_ReturnUnauthorized()
        {
            // Arrange
            var client = factory.CreateClient();
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.AssignReservation(1);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task When_UserDoesntExist_Should_ReturnBadRequest()
        {
            // Arrange
            userResolverMock.Setup(x => x.UserId).Returns(12);
            var client = factory
                .SeedData(dbContext =>
                {
                    dbContext.Reservations.AddRange(ReservationsRequestFactory.CreateReservations());
                    dbContext.Users.Add(ReservationsRequestFactory.CreateUser());
                    dbContext.SaveChanges();
                })
                .Mock(userResolverMock)
                .CreateClient();
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.AssignReservation(1, ReservationsRequestFactory.CreateAuthUser());

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task When_ReservationDoesntExist_Should_ReturnNotFoundStatusCode()
        {
            // Arrange
            var client = factory.SeedData(dbContext =>
            {
                dbContext.Reservations.AddRange(ReservationsRequestFactory.CreateReservations());
                dbContext.SaveChanges();
            }).CreateClient();
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.AssignReservation(2, ReservationsRequestFactory.CreateAuthUser());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task When_AssignedReservation_Should_ReturnOkStatusCode()
        {
            // Arrange
            userResolverMock.Setup(x => x.UserId).Returns(2);
            var client = factory
                .SeedData(dbContext =>
                {
                    dbContext.Reservations.AddRange(ReservationsRequestFactory.CreateReservations());
                    dbContext.Users.Add(ReservationsRequestFactory.CreateUser());
                    dbContext.SaveChanges();
                })
                .Mock(userResolverMock)
                .CreateClient();
            var reservationClient = new ReservationsClient(client);

            // Act
            var response = await reservationClient.AssignReservation(1, ReservationsRequestFactory.CreateAuthUser());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
