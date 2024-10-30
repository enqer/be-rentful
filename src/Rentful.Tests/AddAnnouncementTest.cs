using Rentful.Application.UseCases.Commands.NewApartment;
using Rentful.Domain.Entities;
using Rentful.Domain.Exceptions;

namespace Rentful.Tests
{
    public class AddAnnouncementTest : BaseTest
    {
        public AddAnnouncementTest()
        {

        }

        [Fact]
        public async Task Should_AddNewApartment()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", null, "city", 1);
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act

            var response = await Mediator.Send(command);

            // Assert
            Assert.Single(Repository.Announcements);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_AddApartmentWithThumbnailAsFirstImage()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test1", "test2", "test3" }, false, false, false, false, false, "", null, "city", 1);
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
        public async Task Should_Throws_HttpResponseException_When_UserDoesntExist()
        {
            // Arrange
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string> { "test" }, false, false, false, false, false, "", null, "city", 1);

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(command));
        }

        [Fact]
        public async Task Should_Throws_HttpResponseException_When_ImagesAreEmpty()
        {
            // Arrange
            var user = new User
            {
                Email = "test",
                Id = 1,
            };
            var command = new AddAnnouncementUseCase.Command("test", 1, 2, 1, 2, 1, new List<string>(), false, false, false, false, false, "", null, "city", 1);
            Repository.Users.Add(user);
            Repository.SaveChanges();

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(command));
        }
    }
}
