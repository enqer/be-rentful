using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserResources.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUserResources
{
    public static class GetUserResourcesUseCase
    {
        public record Query : IRequest<ResourcesDto>;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Query, ResourcesDto>
        {
            public async Task<ResourcesDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var announcements = await repository
                    .Announcements
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.Images)
                    .Where(x => x.UserId == userResolver.UserId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                var an = announcements.Where(x => x.DateDeleted != null).Select(x => new AnnouncementDto
                {
                    Id = x.Id,
                    DateAdded = x.DateAdded,
                    Title = x.Title,
                });
                var apartments = announcements.Select(x =>
                {
                    var apartment = x.Apartment;
                    return new ApartmentDto
                    {
                        Area = apartment.Area,
                        HasBalcony = apartment.HasBalcony,
                        HasElevator = apartment.HasElevator,
                        HasParkingSpace = apartment.HasParkingSpace,
                        Id = apartment.Id,
                        IsAnimalFriendly = apartment.IsAnimalFriendly,
                        IsFurnished = apartment.IsFurnished,
                        NumberOfRooms = apartment.NumberOfRooms,
                        Thumbnail = apartment.Images.First(x => x.IsThumbnail).Source
                    };
                });
                return new ResourcesDto
                {
                    Announcements = an,
                    Apartments = apartments
                };
            }
        }
    }
}
