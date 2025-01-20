using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddPayment;

namespace Rentful.Api.Controllers
{
    public class PaymentController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody] AddPaymentUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
