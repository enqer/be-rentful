using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.DeleteAnnouncement
{
    public static class DeleteAnnouncementUseCase
    {
        public record Command(int Id) : IRequest;

        internal class Handler(IRepository repository) : IRequestHandler<Command>
        {

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var announcement = repository.Announcements.FirstOrDefault(x => x.Id == request.Id)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Usunięcie nie powiodło się", "Nie znaleziono ogłoszenia");
                announcement.DateDeleted = DateTime.UtcNow;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
