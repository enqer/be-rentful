﻿using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Commands.AddAnnouncement.Dtos;
using Rentful.Application.UseCases.Commands.NewApartment.Dtos;
using Rentful.Domain.Entities;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.NewApartment
{
    public static class AddAnnouncementUseCase
    {
        public record Command(
            string Title,
            double Price,
            double Rent,
            double Deposit,
            double Area,
            short NumberOfRooms,
            List<string> Images,
            bool IsAnimalFriendly,
            bool IsFurnished,
            bool HasElevator,
            bool HasBalcony,
            bool HasParkingSpace,
            string Description,
            Coordinate? Coordinate,
            string? City,
            string? Province
            ) : IRequest<AddAnnouncementResponse>;

        internal class Handler : IRequestHandler<Command, AddAnnouncementResponse>
        {
            private readonly IRepository repository;
            private readonly IUserResolver userResolver;

            public Handler(IRepository repository, IUserResolver userResolver)
            {
                this.repository = repository;
                this.userResolver = userResolver;
            }

            public async Task<AddAnnouncementResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var images = request.Images.Select((x, index) => new Image
                {
                    Source = x,
                    IsThumbnail = index == 0
                }).ToList();

                if (images.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd dodania oferty", "Ogłoszenie musi posiadać przynajmniej jedno zdjęcie");
                }
                if (request.Coordinate is null && request.City is null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd dodania oferty", "Ogłoszenie musi posiadać lokalizację");
                }
                var user = repository.Users.FirstOrDefault(x => x.Id == userResolver.UserId)
                   ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd użytkownika", "Nie znaleziono użytkownika");

                var location = new Location()
                {
                    IsPrecise = request.Coordinate != null
                };
                if (location.IsPrecise)
                {
                    location.Longitude = request.Coordinate?.Lng ?? 0;
                    location.Latitude = request.Coordinate?.Lat ?? 0;
                }
                else
                {
                    if (request.City?.Length > 0 && request.Province?.Length > 0)
                    {
                        var approximateLocation = repository.Locations.FirstOrDefault(x => x.City.Equals(request.City));
                        location.Province = request.Province;
                        location.City = request.City;
                        location.Longitude = approximateLocation?.Longitude ?? 0;
                        location.Latitude = approximateLocation?.Latitude ?? 0;
                    }
                }

                var apartment = new Apartment
                {
                    Area = request.Area,
                    Deposit = request.Deposit,
                    HasBalcony = request.HasBalcony,
                    HasElevator = request.HasElevator,
                    HasParkingSpace = request.HasParkingSpace,
                    Images = images,
                    IsAnimalFriendly = request.IsAnimalFriendly,
                    IsFurnished = request.IsFurnished,
                    NumberOfRooms = request.NumberOfRooms,
                    Price = request.Price,
                    Rent = request.Rent,
                    Location = location
                };

                var annoucement = new Announcement
                {
                    Apartment = apartment,
                    DateAdded = DateTime.UtcNow,
                    Description = request.Description,
                    Title = request.Title,
                    User = user,
                };
                repository.Announcements.Add(annoucement);
                await repository.SaveChangesAsync(cancellationToken);
                return new AddAnnouncementResponse
                {
                    AnnouncementId = annoucement.Id
                };
            }
        }
    }
}
