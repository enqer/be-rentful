using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Queries.GetLocationsProvince;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetLocationsProvince()
        {
            var query = new GetLocationsProvinceUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
