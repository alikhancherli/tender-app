using FluentValidation;
using Mapster;
using MediatR;
using Tender.App.Application.Commands;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class DefineBidWinnerCommandHandler(IBidRepository bidRepository, IValidator<DefineBidWinnerCommand> validator)
    : IRequestHandler<DefineBidWinnerCommand, ResultHandler<BidDetailsDto>>
{
    public async Task<ResultHandler<BidDetailsDto>> Handle(DefineBidWinnerCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid) return ResultHandler<BidDetailsDto>.Failure(string.Join(" , ", validation.Errors));

        var bid = await bidRepository.GetAsync(request.Id, cancellationToken);
        if (bid is null) return ResultHandler<BidDetailsDto>.Failure("Bid not found!");

        var bidDetail = bid.BidDetails.FirstOrDefault(x => x.Id == request.BidDetailId);
        if (bidDetail is null) return ResultHandler<BidDetailsDto>.Failure("Bid detail not found!");

        bidDetail.SetWinner();

        bid.AddBidDetail(bidDetail);

        await bidRepository.UpdateAsync(bid);
        await bidRepository.SaveAndDispatchEventsAsync(cancellationToken);

        var mappedModel = bidDetail.Adapt<BidDetailsDto>();
        return ResultHandler<BidDetailsDto>.Success(mappedModel);
    }
}
