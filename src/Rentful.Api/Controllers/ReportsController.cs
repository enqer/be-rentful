using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddReport;
using Rentful.Application.UseCases.Commands.ResolveReport;
using Rentful.Application.UseCases.Queries.GetReportsByLeaseAgreement;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ReportsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{agreementId}")]
        public async Task<ActionResult> GetReportsByLeaseAgreement(int agreementId)
        {
            var query = new GetReportsByLeaseAgreementUseCase.Query(agreementId);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody] AddReportUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> ResolveReport([FromBody] ResolveReportUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
