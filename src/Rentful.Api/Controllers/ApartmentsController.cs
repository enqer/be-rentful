using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddTenantToApartment;
using Rentful.Application.UseCases.Queries.GetApartmentById;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ApartmentsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{apartmentId}")]
        public async Task<IActionResult> GetAnnouncementById(int apartmentId)
        {
            var query = new GetApartmentByIdUseCase.Query(apartmentId);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("add-tenant")]
        public async Task<IActionResult> AddTenantToApartment([FromBody] AddTenantToApartmentUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

    }
}
