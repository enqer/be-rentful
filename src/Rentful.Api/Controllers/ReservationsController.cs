using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddReservations;
using Rentful.Application.UseCases.Commands.AssignReservation;
using Rentful.Application.UseCases.Commands.CancelReservation;
using Rentful.Application.UseCases.Commands.CancelReservationByOwner;
using Rentful.Application.UseCases.Queries.GetApartmentReservations;
using Rentful.Application.UseCases.Queries.GetUserReservations;

namespace Rentful.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ReservationsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetUserReservations()
        {
            var query = new GetUserReservationsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{apartmentId}")]
        public async Task<ActionResult> GetApartmentReservations(int apartmentId)
        {
            var query = new GetApartmentReservationsUseCase.Query(apartmentId);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("{reservationId}")]
        public async Task<ActionResult> AssignReservation(int reservationId)
        {
            var command = new AssignReservationUseCase.Command(reservationId);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("{reservationId}/cancel")]
        public async Task<ActionResult> CancelReservation(int reservationId)
        {
            var command = new CancelReservationUseCase.Command(reservationId);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("{reservationId}/cancel-by-owner")]
        public async Task<ActionResult> CancelReservationByOwner(int reservationId)
        {
            var command = new CancelReservationByOwnerUseCase.Command(reservationId);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddReservations([FromBody] AddReservationsUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
