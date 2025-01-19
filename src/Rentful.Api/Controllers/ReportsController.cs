using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.AddReport;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ReportsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody] AddReportUseCase.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
