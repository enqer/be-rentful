using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.NewApartment;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnnouncementController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement([FromBody] AddAnnouncementUseCase.Command command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
