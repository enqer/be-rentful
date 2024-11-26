using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Queries.GetLocationsProvince;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProvinceCities()
        {
            var query = new GetProvinceCitiesUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
