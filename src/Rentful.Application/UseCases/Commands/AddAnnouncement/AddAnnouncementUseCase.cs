using MediatR;
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
            string? Province,
            List<string> Reservations
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
                    City = request.City ?? string.Empty,
                    Province = request.Province ?? string.Empty,
                    Latitude = request?.Coordinate?.Lat ?? 0,
                    Longitude = request?.Coordinate?.Lng ?? 0,
                    IsPrecise = request?.City == null && request?.Coordinate != null
                };
                if ((request?.Province ?? string.Empty).Length <= 0 && request.Coordinate?.Lat > 0 && request.Coordinate?.Lng > 0)
                {
                    var closestCity = repository.Locations
                       .Where(x => x.Latitude != 0 && x.Longitude != 0)
                       .ToList()
                       .Select(x => new
                       {
                           Location = x,
                           Distance = GetDistance((double)request.Coordinate.Lat, (double)request.Coordinate.Lng, (double)x.Latitude, (double)x.Longitude)
                       })
                       .OrderBy(x => x.Distance)
                       .FirstOrDefault()?.Location;
                    if (closestCity != null)
                    {
                        location.Province = closestCity.Province;
                        location.City = closestCity.City;

                    }

                }
                else if ((request?.Coordinate?.Lat ?? 0) == 0 || (request?.Coordinate?.Lng ?? 0) == 0)
                {
                    var similarLocation = repository.Locations.FirstOrDefault(x => x.City == request.City);
                    if (similarLocation != null)
                    {
                        location.Longitude = similarLocation.Longitude;
                        location.Latitude = similarLocation.Latitude;
                    }

                }

                var apartment = new Apartment
                {
                    Area = request!.Area,
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
            private double GetDistance(double lat1, double lng1, double lat2, double lng2)
            {
                const double EarthRadiusKm = 6371;

                var dLat = DegreesToRadians(lat2 - lat1);
                var dLng = DegreesToRadians(lng2 - lng1);

                var a = Math.Sin((dLat / 2)) * Math.Sin((dLat / 2)) +
                            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                            Math.Sin((dLng / 2)) * Math.Sin((dLng / 2));

                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt((1 - a)));
                return EarthRadiusKm * c;
            }

            private double DegreesToRadians(double degrees)
            {
                return degrees * Math.PI / 180;
            }
        }
    }
}
