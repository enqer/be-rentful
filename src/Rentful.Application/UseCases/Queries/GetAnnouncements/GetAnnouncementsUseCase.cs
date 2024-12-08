using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetAnnouncements.Dtos;

namespace Rentful.Application.UseCases.Queries.GetAnnouncements
{
    public static class GetAnnouncementsUseCase
    {
        public record Query : IRequest<IEnumerable<AnnouncementDto>>;

        internal class Handler : IRequestHandler<Query, IEnumerable<AnnouncementDto>>
        {
            private readonly IRepository repository;
            public Handler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<AnnouncementDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var announcements = repository
                    .Announcements
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.Location)
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.Images)
                    .ToList();

                var announcementsConverted = announcements.Select(x =>
                    new AnnouncementDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Price = x.Apartment.Price,
                        Rent = x.Apartment.Rent ?? 0,
                        Area = x.Apartment.Area,
                        DateAdded = x.DateAdded,
                        HasBalcony = x.Apartment.HasBalcony,
                        HasElevator = x.Apartment.HasElevator,
                        HasParkingSpace = x.Apartment.HasParkingSpace,
                        Image = x.Apartment.Images.FirstOrDefault(x => x.IsThumbnail)?.Source ?? string.Empty,
                        IsAnimalFriendly = x.Apartment.IsAnimalFriendly,
                        IsFurnished = x.Apartment.IsFurnished,
                        NumberOfRooms = x.Apartment.NumberOfRooms,
                        Location = new LocationDto
                        {
                            City = x.Apartment.Location.City,
                            IsPrecise = x.Apartment.Location.IsPrecise,
                            Province = x.Apartment.Location.Province,
                            Latitude = x.Apartment.Location.Latitude,
                            Longitude = x.Apartment.Location.Longitude
                        }
                    }
                );
                return await Task.FromResult(announcementsConverted);
            }
        }
    }
}
