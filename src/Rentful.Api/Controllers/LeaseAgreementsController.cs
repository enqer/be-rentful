using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentful.Application.UseCases.Commands.SetLeaseAgreementStatus;
using Rentful.Application.UseCases.Commands.SetTenantRating;
using Rentful.Domain.Entities.Enums;

namespace Rentful.Api.Controllers
{
    [ApiController]
    [Route("api/v1/lease-agreements")]
    [Authorize]
    public class LeaseAgreementsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{agreementId}/{status}")]
        public async Task<IActionResult> SetLeaseAgreementStatus(int agreementId, LeaseAgreementStatusEnum status)
        {
            var command = new SetLeaseAgreementStatusUseCase.Command(agreementId, status);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{agreementId}/tenant/{rate}")]
        public async Task<IActionResult> SetTenantRating(int agreementId, TenantRatingEnum rate)
        {
            var command = new SetTenantRatingUseCase.Command(agreementId, rate);
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("{agreementId}/tenant/raport")]
        public async Task<IActionResult> GetTenantRaport(int agreementId, TenantRatingEnum rate)
        {
            var command = new SetTenantRatingUseCase.Command(agreementId, rate);
            await mediator.Send(command);
            return Ok();
        }

    }
}
