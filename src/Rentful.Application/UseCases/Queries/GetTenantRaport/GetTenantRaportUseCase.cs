using MediatR;
using Rentful.Application.Common.Interfaces;

namespace Rentful.Application.UseCases.Queries.GetTenantRaport
{
    public static class GetTenantRaportUseCase
    {
        public record Query : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Query>
        {
            public Task Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
