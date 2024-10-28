using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Commands.NewApartment.Dtos;

namespace Rentful.Application.UseCases.Commands.NewApartment
{
    public static class AddNewApartmentUseCase
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
            bool HasElevator,
            bool IsFurnished,
            string Description,
            Coordinate? Coordinate,
            string? City
            ) : IRequest;

        internal class Handler : IRequestHandler<Command>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {

            }
        }
    }
}
