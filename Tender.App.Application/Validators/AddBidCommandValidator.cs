using FluentValidation;
using Tender.App.Application.Commands;

namespace Tender.App.Application.Validators;

public sealed class AddBidCommandValidator : AbstractValidator<AddBidCommand>
{
    private DateTime nowTime;
    public AddBidCommandValidator()
    {
        nowTime = DateTime.Now;

        RuleFor(a => a.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(a => a.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(a => a.MinAmount)
            .NotNull()
            .NotEmpty()
            .GreaterThan(-1)
            .Must((cmd, val) => cmd.MaxAmount > val);

        RuleFor(a => a.MaxAmount)
            .NotNull()
            .NotEmpty()
            .GreaterThan(-1)
            .Must((cmd, val) => cmd.MinAmount < val);

        RuleFor(a => a.StartIn)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(nowTime)
            .Must((cmd, val) => cmd.EndIn > val);

        RuleFor(a => a.EndIn)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(nowTime)
            .Must((cmd, val) => cmd.StartIn < val);
    }
}
