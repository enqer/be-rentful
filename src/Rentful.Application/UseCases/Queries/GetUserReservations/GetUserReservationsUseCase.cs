using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetUserReservations.Dtos;

namespace Rentful.Application.UseCases.Queries.GetUserReservations
{
    public static class GetUserReservationsUseCase
    {
        public record Query : IRequest<List<ReservationDto>>;

        internal class Handler(IRepository repository, IUserResolver userResolver) : IRequestHandler<Query, List<ReservationDto>>
        {
            public async Task<List<ReservationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reservations = await repository.Reservations.Where(x => x.UserId == userResolver.UserId).AsNoTracking().ToListAsync(cancellationToken);
                return reservations.ConvertAll(x => new ReservationDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Status = x.Status,
                    AnnouncementId = x.AnnouncementId
                });
            }
        }
    }
}
