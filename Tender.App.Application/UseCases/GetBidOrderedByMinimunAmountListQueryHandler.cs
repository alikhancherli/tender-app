using Mapster;
using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class GetBidOrderedByMinimunAmountListQueryHandler(IBidRepository bidRepository)
    : IRequestHandler<GetBidOrderedByMinimunAmountListQuery, ResultHandler<IList<BidFilteredDto>>>
{
    public async Task<ResultHandler<IList<BidFilteredDto>>> Handle(GetBidOrderedByMinimunAmountListQuery request, CancellationToken cancellationToken)
    {
        var bids = await bidRepository.GetListAsync(_ => true, cancellationToken);
        
        var mappedModel = bids.Adapt<IList<BidFilteredDto>>();

        return ResultHandler<IList<BidFilteredDto>>.Success(mappedModel);
    }
}
