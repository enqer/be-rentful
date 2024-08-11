using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.GenerateToken.Dtos;
using Rentful.Application.UseCases.Commands.LoginUser;
using Rentful.Application.UseCases.Commands.RegisterUser;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto register, CancellationToken cancellationToken)
        {
            var command = new RegisterUserUseCase.Command(register);
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
