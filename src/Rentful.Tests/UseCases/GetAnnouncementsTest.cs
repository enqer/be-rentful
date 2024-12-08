using Rentful.Application.UseCases.Queries.GetAnnouncements;
using Rentful.Domain.Entities;
using Rentful.Tests.Common;

namespace Rentful.Tests.UseCases
{
    public class GetAnnouncementsTest : BaseTest
    {
        [Fact]
        public async Task Should_ReturnAnnouncements()
        {
            // Arrange
            var announcements = new List<Announcement>()
            {
                new Announcement
                {
                    Id = 1,
                    Title = "New apartment",
                    DateAdded = DateTime.UtcNow,
                    Apartment = new Apartment
                    {
                        Id = 1,
                        Price = 1230
                    }
                },
                new Announcement
                {
                    Id = 2,
                    Title = "Cheap apartments with 4 rooms",
                    DateAdded = DateTime.UtcNow,
                    Apartment = new Apartment
                    {
                        Id = 2,
                        Price = 1230,
                        NumberOfRooms = 4
                    }
                }
            };

            var query = new GetAnnouncementsUseCase.Query();
            Repository.Announcements.AddRange(announcements);
            Repository.SaveChanges();

            // Act
            var response = await Mediator.Send(query);

            // Assert
            Assert.Equal(2, response.Count());
        }
    }
}
