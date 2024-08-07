using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.LoginUser;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] AuthUserUseCase.Command command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUser([FromBody] AuthUserUseCase.Command command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
