using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetLocationsProvince.Dtos;
using Rentful.Application.UseCases.Queries.GetProvinceCities.Dtos;

namespace Rentful.Application.UseCases.Queries.GetLocationsProvince
{
    public static class GetProvinceCitiesUseCase
    {
        public record Query : IRequest<IEnumerable<ProvinceCitiesDto>>;

        internal class Handler : IRequestHandler<Query, IEnumerable<ProvinceCitiesDto>>
        {
            private readonly IRepository repository;
            public Handler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<ProvinceCitiesDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var locations = repository.Locations.AsNoTracking().Where(x => !x.IsPrecise).ToList();
                var locationsGrouped = locations.GroupBy(
                    x => x.Province,
                    (province, cities) => new ProvinceCitiesDto
                    {
                        Province = province,
                        Cities = cities.ToList().Select(x =>
                            new CityDto
                            {
                                Name = x.City,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                            }
                        )
                    });
                return await Task.FromResult(locationsGrouped);
            }
        }
    }
}
