using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AssignReservation;

namespace Rentful.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReservationsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{reservationId}")]
        public async Task<ActionResult> AssignReservation(int reservationId)
        {
            var command = new AssignReservationUseCase.Command(reservationId);
            await mediator.Send(command);
            return Ok();
        }
    }
}
