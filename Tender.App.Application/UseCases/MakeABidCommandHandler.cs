using FluentValidation;
using MediatR;
using Tender.App.Application.Commands;
using Tender.App.Domain.Entities;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class MakeABidCommandHandler(IBidRepository bidRepository, IValidator<MakeABidCommand> validator)
    : IRequestHandler<MakeABidCommand, ResultHandler<bool>>
{
    public async Task<ResultHandler<bool>> Handle(MakeABidCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid) return ResultHandler<bool>.Failure(string.Join(" , ", validation.Errors));

        var bid = await bidRepository.GetAsync(request.BidId, cancellationToken);
        if (bid is null) return ResultHandler<bool>.Failure("Bid not found!");

        if (bid.IsAccomplished()) return ResultHandler<bool>.Failure("This bid has been finished!");

        if (!bid.IsActive()) return ResultHandler<bool>.Failure("This bid is not active!");

        if (bid.HasWinner()) return ResultHandler<bool>.Failure("This bid has winner! You can't make a bid.");

        if(!bid.IsAmountInRange(request.Amount)) return ResultHandler<bool>.Failure("Amount is not in min and max range.");

        var bidDetail = new BidDetails(request.UserId, request.Amount);
        bid.AddBidDetail(bidDetail);

        await bidRepository.UpdateAsync(bid);
        await bidRepository.SaveAndDispatchEventsAsync(cancellationToken);

        return ResultHandler<bool>.Success(true);
    }
}
