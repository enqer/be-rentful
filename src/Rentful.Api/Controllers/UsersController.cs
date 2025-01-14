using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.ChangePassword;
using Rentful.Application.UseCases.Queries.GetUserInfo;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {

        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var query = new GetUserInfoUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
