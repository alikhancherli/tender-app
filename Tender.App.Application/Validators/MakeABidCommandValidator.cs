using FluentValidation;
using Tender.App.Application.Commands;
using Tender.App.Domain.Repositories;

namespace Tender.App.Application.Validators;

public sealed class MakeABidCommandValidator : AbstractValidator<MakeABidCommand>
{
    private readonly IBidRepository _bidRepository;
    public MakeABidCommandValidator(IBidRepository bidRepository)
    {
        _bidRepository = bidRepository;

        RuleFor(a => a.BidId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(a => a.UserId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .MustAsync(MustBeUniquUserAndAmount)
            .WhenAsync(MustNotBeNull)
            .WithMessage("You have been sent the same amount!");

        RuleFor(a => a.Amount)
            .NotNull()
            .NotEmpty();
    }

    private async Task<bool> MustBeUniquUserAndAmount(MakeABidCommand command, int userId, CancellationToken cancellationToken)
    {
        var bid = await _bidRepository.GetAsync(_ => _.Id == command.BidId, cancellationToken);
        if (bid is null) return false;
        if (bid.BidDetails.Any(_ => _.UserId == userId && _.Amount.Value == command.Amount)) return false;

        return true;
    }

    private async Task<bool> MustNotBeNull(MakeABidCommand command, CancellationToken cancellationToken)
    {
        var bid = await _bidRepository.GetAsync(_ => _.Id == command.BidId, cancellationToken);
        if (bid is null) return false;
        return true;
    }
}
