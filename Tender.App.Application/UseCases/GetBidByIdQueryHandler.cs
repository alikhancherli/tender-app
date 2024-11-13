using Mapster;
using MapsterMapper;
using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Application.Queries;
using Tender.App.Domain.Entities;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class GetBidByIdQueryHandler(IBidRepository bidRepository) : IRequestHandler<GetBidByIdQuery, ResultHandler<BidDto>>
{
    public async Task<ResultHandler<BidDto>> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
    {
        var bid = await bidRepository.GetAsync(request.Id, cancellationToken);
        if (bid is null) return ResultHandler<BidDto>.Failure("Bid not found!");
        var mappedModel = bid.Adapt<BidDto>();
        return ResultHandler<BidDto>.Success(mappedModel);
    }
}
