using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetLocationsProvince.Dtos;

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
                var locations = repository.Locations.Where(x => !x.IsPrecise).ToList();
                var locationsGrouped = locations.GroupBy(x => x.Province, x => x.City, (key, city) => new ProvinceCitiesDto { Province = key, Cities = city });
                return await Task.FromResult(locationsGrouped);
            }
        }
    }
}
