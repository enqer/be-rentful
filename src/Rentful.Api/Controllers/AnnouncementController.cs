using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.NewApartment;
using Rentful.Application.UseCases.Queries.GetAnnouncements;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnnouncementController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var query = new GetAnnouncementsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement([FromBody] AddAnnouncementUseCase.Command command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
