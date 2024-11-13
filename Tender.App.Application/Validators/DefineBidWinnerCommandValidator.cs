using FluentValidation;
using Tender.App.Application.Commands;

namespace Tender.App.Application.Validators;

public sealed class DefineBidWinnerCommandValidator : AbstractValidator<DefineBidWinnerCommand>
{
    public DefineBidWinnerCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(a => a.BidDetailId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);
    }
}
