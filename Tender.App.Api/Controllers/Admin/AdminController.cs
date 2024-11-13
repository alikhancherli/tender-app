using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tender.App.Api.Models.Admin;
using Tender.App.Application.Commands;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Shared;

namespace Tender.App.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<ResultHandler<BidDto>> AddAsync(
            [FromBody] AddBidRequest request,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new AddBidCommand(
                request.Title,
                request.Description,
                request.StartIn,
                request.EndIn,
                request.MinAmount,
                request.MaxAmount),
            cancellationToken);

            return result;
        }

        [HttpPost("define-winner")]
        public async Task<ResultHandler<BidDetailsDto>> DefineWinnerAsync(
            [FromBody] DefineWinnerRequest request,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DefineBidWinnerCommand(
                request.Id,
                request.BidDetailId),
            cancellationToken);

            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<ResultHandler<BidDto>> GetBidByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBidByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpGet("list")]
        public async Task<ResultHandler<IList<BidDto>>> GetBidListAsync(
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBidListQuery(), cancellationToken);
            return result;
        }

        [HttpGet("accomplished-list")]
        public async Task<ResultHandler<IList<BidDto>>> GetBidAccomplishedListAsync(
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAccomplishedBidListQuery(), cancellationToken);
            return result;
        }
    }
}
