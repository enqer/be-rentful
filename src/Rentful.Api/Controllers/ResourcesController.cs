using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Queries.GetUserResources;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ResourcesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserResources()
        {
            var query = new GetUserResourcesUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
