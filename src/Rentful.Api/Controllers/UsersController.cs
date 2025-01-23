using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.ChangePassword;
using Rentful.Application.UseCases.Commands.ChangeUserAddress;
using Rentful.Application.UseCases.Commands.RemindPassword;
using Rentful.Application.UseCases.Commands.SendMailToUser;
using Rentful.Application.UseCases.Queries.GetTenantApartments;
using Rentful.Application.UseCases.Queries.GetUseLeaseAgreements;
using Rentful.Application.UseCases.Queries.GetUserAddress;
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

        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var query = new GetUserAddressUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("lease-agreements")]
        public async Task<IActionResult> GetUserLeaseAgreements()
        {
            var query = new GetUserLeaseAgreementsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("apartments")]
        public async Task<IActionResult> GetApartments()
        {
            var query = new GetTenantApartmentsUseCase.Query();
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpPost("{email}/reset-password")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var command = new ResetPasswordUseCase.Command(email);
            await mediator.Send(command);
            return Ok();
        }
        
        [HttpPost("send-email")]
        public async Task<IActionResult> SendMailToUser([FromBody] SendMailToUserUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
        [HttpPut("password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("address")]
        public async Task<IActionResult> ChangeUserAddres([FromBody] ChangeUserAddressUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }


    }
}
