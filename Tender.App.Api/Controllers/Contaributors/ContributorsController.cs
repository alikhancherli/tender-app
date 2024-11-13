using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tender.App.Application.Commands;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Shared;

namespace Tender.App.Api.Controllers.Contaributors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<ResultHandler<IList<BidDto>>> GetBidListAsync(
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBidListQuery(), cancellationToken);
            return result;
        }

        [HttpGet("minimun-amount")]
        public async Task<ResultHandler<IList<BidFilteredDto>>> GetBidOrderedByMinimunAmountListAsync(
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBidOrderedByMinimunAmountListQuery(), cancellationToken);
            return result;
        }

        [HttpPost("make-bid/{id:int}")]
        public async Task<ResultHandler<bool>> MakeABidAsync(
            int id,
            [Required] long amount,
            CancellationToken cancellationToken)
        {
            var randomUserID = new Random(1);
            var userId = randomUserID.Next(1, 6);
            var result = await mediator.Send(new MakeABidCommand(
                id,
                userId,
                amount),
            cancellationToken);

            return result;
        }
    }
}
