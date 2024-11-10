﻿using Rentful.Application.UseCases.Queries.GetLocationsProvince;
using Rentful.Domain.Entities;

namespace Rentful.Tests
{
    public class GetProvinceCitiesTest : BaseTest
    {
        [Fact]
        public async Task When_UserDoesntExist_Should_Throws_HttpResponseException()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location
                {
                    Id = 1,
                    City = "Cracow",
                    Province = "Lesser Poland",
                    IsPrecise = false,
                },
                 new Location
                {
                    Id = 2,
                    City = "Tarnow",
                    Province = "Lesser Poland",
                    IsPrecise = false,
                },
                  new Location
                {
                    Id = 3,
                    City = "Warsaw",
                    Province = "Masovian Voivodeship",
                    IsPrecise = false,
                },
            };
            Repository.Locations.AddRange(locations);
            await Repository.SaveChangesAsync();
            var query = new GetProvinceCitiesUseCase.Query();

            // Act
            var response = await Mediator.Send(query);

            // Assert
            var numberOfLesserPolandCities = response.Where(x => x.Province == "Lesser Poland").SelectMany(x => x.Cities).Count();
            Assert.Equal(2, response.Count());
            Assert.Equal(2, numberOfLesserPolandCities);
        }
    }
}