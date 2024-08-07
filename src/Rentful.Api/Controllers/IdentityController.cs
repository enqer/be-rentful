using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.GenerateTokenUseCase;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenUseCase.Command command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
