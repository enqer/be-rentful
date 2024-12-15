using Rentful.Application.UseCases.Queries.GetAnnouncementById;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using Rentful.Tests.Common;

namespace Rentful.Tests.UseCases
{
    public class GetAnnouncementByIdTest : BaseTest
    {

        [Fact]
        public async Task Should_ReturnAnnouncementById()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Cracow",
                Province = "Lesser Poland",
                Latitude = 123.123123M,
                Longitude = 32.123M,
                IsPrecise = false,
            };
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    Date = "12-12-2012 12:12",
                    Status = ReservationStatusEnum.Unresolved
                }
            };
            var apartment = new Apartment()
            {
                Id = 1,
                Area = 30,
                Deposit = 123,
                HasBalcony = true,
                Images = new List<Image>
                {
                    new Image()
                    {
                        Id = 1,
                        IsThumbnail = true,
                        Source = "base64;data"
                    }
                },
                Location = new Location
                {
                    Id = 2,
                    City = "Cracow",
                    Province = "Lesser Poland",
                    IsPrecise = false,
                },
                NumberOfRooms = 3,
                Price = 1232.3,
                IsAnimalFriendly = true,
                HasElevator = true,
                HasParkingSpace = false,
                IsFurnished = true,
                Rent = 12
            };
            var announcement = new Announcement()
            {
                Id = 1,
                Apartment = apartment,
                DateAdded = DateTime.UtcNow,
                Description = "descr",
                Title = "This is title",
                User = new User()
                {
                    Id = 1,
                    Email = "rick@sorkin.com",
                    FirstName = "Rick",
                    LastName = "Sorkin"
                },
                Reservations = reservations
            };

            var query = new GetAnnouncementByIdUseCase.Query(1);

            Repository.Locations.Add(location);
            Repository.Announcements.Add(announcement);
            await Repository.SaveChangesAsync();
            // Act
            var response = await Mediator.Send(query);

            // Assert
            Assert.Equal(1232.3, response.Price);
            Assert.Equal("Cracow", response.City);
            Assert.True(response.IsFurnished);
            Assert.Single(response.Images);
        }

        [Fact]
        public async Task Should_ReturnAnnouncementByIdWithReservationsDifferentThanApproved()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Cracow",
                Province = "Lesser Poland",
                Latitude = 123.123123M,
                Longitude = 32.123M,
                IsPrecise = false,
            };
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    Date = "12-12-2012 12:12",
                    Status = ReservationStatusEnum.Unresolved
                },
                new Reservation
                {
                    Id = 2,
                    Date = "14-12-2012 12:12",
                    Status = ReservationStatusEnum.Approved
                },
                new Reservation
                {
                    Id = 3,
                    Date = "14-12-2012 14:12",
                    Status = ReservationStatusEnum.Unapproved
                },
                new Reservation
                {
                    Id = 4,
                    Date = "11-12-2012 14:12",
                    Status = ReservationStatusEnum.Available
                },
                new Reservation
                {
                    Id = 5,
                    Date = "12-11-2012 12:14",
                    Status = ReservationStatusEnum.Available
                },
            };
            var apartment = new Apartment()
            {
                Id = 1,
                Area = 30,
                Deposit = 123,
                HasBalcony = true,
                Images = new List<Image>
                {
                    new Image()
                    {
                        Id = 1,
                        IsThumbnail = true,
                        Source = "base64;data"
                    }
                },
                Location = new Location
                {
                    Id = 2,
                    City = "Cracow",
                    Province = "Lesser Poland",
                    IsPrecise = false,
                },
                NumberOfRooms = 3,
                Price = 1232.3,
                IsAnimalFriendly = true,
                HasElevator = true,
                HasParkingSpace = false,
                IsFurnished = true,
                Rent = 12
            };
            var announcement = new Announcement()
            {
                Id = 1,
                Apartment = apartment,
                DateAdded = DateTime.UtcNow,
                Description = "descr",
                Title = "This is title",
                User = new User()
                {
                    Id = 1,
                    Email = "rick@sorkin.com",
                    FirstName = "Rick",
                    LastName = "Sorkin"
                },
                Reservations = reservations
            };

            var query = new GetAnnouncementByIdUseCase.Query(1);

            Repository.Locations.Add(location);
            Repository.Announcements.Add(announcement);
            await Repository.SaveChangesAsync();
            // Act
            var response = await Mediator.Send(query);

            // Assert
            Assert.Equal(1232.3, response.Price);
            Assert.Equal("Cracow", response.City);
            Assert.True(response.IsFurnished);
            Assert.Equal(2, response.Reservations.Count);
            Assert.Single(response.Images);
        }

        [Fact]
        public async Task When_AnnouncementDoesntExist_Should_ThrowHttpResponseException()
        {
            // Arrange
            var query = new GetAnnouncementByIdUseCase.Query(19);

            // Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(async () => await Mediator.Send(query));
        }
    }
}
