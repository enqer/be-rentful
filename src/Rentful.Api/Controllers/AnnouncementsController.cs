using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.DeleteAnnouncement;
using Rentful.Application.UseCases.Commands.NewApartment;
using Rentful.Application.UseCases.Queries.GetAnnouncementById;
using Rentful.Application.UseCases.Queries.GetAnnouncements;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AnnouncementsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var query = new GetAnnouncementsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{announcementId}")]
        public async Task<IActionResult> GetAnnouncementById(int announcementId)
        {
            var query = new GetAnnouncementByIdUseCase.Query(announcementId);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement([FromBody] AddAnnouncementUseCase.Command command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{announcementId}")]
        public async Task<IActionResult> DeleteAnnouncement(int announcementId)
        {
            var command = new DeleteAnnouncementUseCase.Command(announcementId);
            await mediator.Send(command);
            return Ok();
        }


    }
}
