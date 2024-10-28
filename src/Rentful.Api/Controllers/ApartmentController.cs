using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.RegisterUser;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApartmentController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddNewApartment([FromBody] RegisterUserUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }


    }
}
