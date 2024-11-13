using Mapster;
using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class GetAccomplishedBidListQueryHandler(IBidRepository bidRepository)
    : IRequestHandler<GetAccomplishedBidListQuery, ResultHandler<IList<BidDto>>>
{
    public async Task<ResultHandler<IList<BidDto>>> Handle(GetAccomplishedBidListQuery request, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.Now;
        var bids = await bidRepository.GetListAsync(_ => currentDate >= _.StartIn && currentDate >= _.EndIn, cancellationToken);

        var mappedModel = bids.Adapt<IList<BidDto>>();

        return ResultHandler<IList<BidDto>>.Success(mappedModel);
    }
}