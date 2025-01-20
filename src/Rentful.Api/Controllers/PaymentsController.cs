using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddPayment;
using Rentful.Application.UseCases.Queries.GetPaymentsByLeaseAgreement;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{agreementId}")]
        public async Task<ActionResult> GetPaymentsByLeaseAgreement(int agreementId)
        {
            var query = new GetPaymentsByLeaseAgreementUseCase.Query(agreementId);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

    }
}
