using Moq;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Commands.NewApartment;
using Rentful.Application.UseCases.Commands.NewApartment.Dtos;
using Rentful.Domain.Entities;
using Rentful.Domain.Exceptions;
using Rentful.Tests.Common;

namespace Rentful.Tests.UseCases
{
    public class AddAnnouncementTest : BaseTest
    {
        private readonly Mock<IUserResolver> userResolverMock = new Mock<IUserResolver>();
        public AddAnnouncementTest()
        {
            Mock(userResolverMock);
            userResolverMock.Setup(x => x.UserId).Returns(1);
        }

        [Fact]
        public async Task Should_AddNewAnnouncement()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var coords = new Coordinate
            {
                Lat = 53.13213M,
                Lng = 19.323M,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", coords, null, null);
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act
            var response = await Mediator.Send(command);

            // Assert
            Assert.Single(Repository.Announcements);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task When_CityIsMissing_Should_AddNewAnnouncementWithClosestCity()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var coords = new Coordinate
            {
                Lat = 53.13213M,
                Lng = 21.323M,
            };
            var locations = new List<Location>()
            {
                new Location()
                {
                    City = "Warszawa",
                    Id = 1,
                    Latitude = 52.237049M,
                    Longitude = 21.017532M,
                    Province = "Mazowieckie"
                },
                new Location()
                {
                    City = "Kraków",
                    Id = 2,
                    Latitude = 50.049683M,
                    Longitude = 19.944544M,
                    Province = "Małopolskie"
                }
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", coords, null, null);
            Repository.Users.Add(user);
            Repository.Locations.AddRange(locations);
            Repository.SaveChanges();

            // Act
            var response = await Mediator.Send(command);

            // Assert
            var announcement = Assert.Single(Repository.Announcements);
            Assert.NotNull(response);
            Assert.Equal("Warszawa", announcement.Apartment.Location.City);
        }

        [Fact]
        public async Task When_CityCoordsAreMissing_Should_AddNewAnnouncementWithCoordsByCity()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var locations = new List<Location>()
            {
                new Location()
                {
                    City = "Warszawa",
                    Id = 1,
                    Latitude = 52.237049M,
                    Longitude = 21.017532M,
                    Province = "Mazowieckie"
                }
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", null, "Warszawa", "Mazowieckie");
            Repository.Users.Add(user);
            Repository.Locations.AddRange(locations);
            Repository.SaveChanges();

            // Act
            var response = await Mediator.Send(command);

            // Assert
            var announcement = Assert.Single(Repository.Announcements);
            Assert.NotNull(response);
            Assert.Equal(52.237049M, announcement.Apartment.Location.Latitude);
        }


        [Fact]
        public async Task Should_AddAnnouncementWithThumbnailAsFirstImage()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var coords = new Coordinate
            {
                Lat = 53.13213M,
                Lng = 19.323M,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test1", "test2", "test3" }, false, false, false, false, false, "", coords, "city", "province");
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act
            var response = await Mediator.Send(command);

            // Assert
            var announcement = Repository.Announcements.Single();
            var thumbnails = announcement.Apartment.Images.Where(x => x.IsThumbnail);
            Assert.NotNull(response);
            Assert.Equal("test1", thumbnails.Single().Source);
        }

        [Fact]
        public async Task When_UserDoesntExist_Should_Throws_HttpResponseException()
        {
            // Arrange
            var coords = new Coordinate
            {
                Lat = 53.13213M,
                Lng = 19.323M,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", coords, "city", "province");

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(command));
        }

        [Fact]
        public async Task When_ImagesAreEmpty_Should_Throws_HttpResponseException()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string>(), false, false, false, false, false, "", null, "city", "province");
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(command));
        }
        [Fact]
        public async Task When_LocationIsNull_Should_Throws_HttpResponseException()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string>() { "image" }, false, false, false, false, false, "", null, null, null);
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(command));
        }
    }
}
