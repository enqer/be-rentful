using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.LoginUser;
using Rentful.Application.UseCases.Commands.RegisterUser;


namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUser([FromBody] AuthUserUseCase.Command command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
