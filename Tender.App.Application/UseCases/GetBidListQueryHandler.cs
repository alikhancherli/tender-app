using Mapster;
using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class GetBidListQueryHandler(IBidRepository bidRepository) : IRequestHandler<GetBidListQuery, ResultHandler<IList<BidDto>>>
{
    public async Task<ResultHandler<IList<BidDto>>> Handle(GetBidListQuery request, CancellationToken cancellationToken)
    {
        var bids = await bidRepository.GetListAsync(_ => true, cancellationToken);

        var mappedModel = bids.Adapt<IList<BidDto>>();

        return ResultHandler<IList<BidDto>>.Success(mappedModel);
    }
}
