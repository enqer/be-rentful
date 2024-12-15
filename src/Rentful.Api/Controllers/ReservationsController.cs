using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AssignReservation;
using Rentful.Application.UseCases.Queries.GetUserReservations;

namespace Rentful.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ReservationsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{reservationId}")]
        public async Task<ActionResult> AssignReservation(int reservationId)
        {
            var command = new AssignReservationUseCase.Command(reservationId);
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserReservations()
        {
            var query = new GetUserReservationsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
