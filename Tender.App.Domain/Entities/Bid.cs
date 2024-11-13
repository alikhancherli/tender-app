using Tender.App.Domain.Events;
using Tender.App.Domain.Exceptions;
using Tender.App.Domain.Shared;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Domain.Entities;

public sealed class Bid : AggregateRoot<int>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime StartIn { get; private set; }
    public DateTime EndIn { get; private set; }
    public AmountValueObject MinAmount { get; private set; }
    public AmountValueObject MaxAmount { get; private set; }
    public IList<BidDetails> BidDetails { get; private set; } = new List<BidDetails>();

    private Bid() { }

    public Bid(
        string title,
        string description,
        DateTime startIn,
        DateTime endIn,
        long minAmount,
        long maxAmount)
    {
        SetTitle(title);
        SetDescription(description);
        SetStartIn(startIn);
        SetEndIn(endIn);
        SetAmount(minAmount, maxAmount);

        CreatedTimeUtc = DateTimeOffset.UtcNow;
        AddEvent(new BidAddedEvent(this));
    }

    private void SetTitle(string title)
    {
        if (title is null) throw new ArgumentNullException(nameof(title));
        Title = title;
    }

    private void SetDescription(string description)
    {
        if (description is null) throw new ArgumentNullException(nameof(description));
        Description = description;
    }

    private void SetStartIn(DateTime startIn)
    {
        if (startIn < DateTime.Now) throw new ValueOutOfRangeException(nameof(startIn));
        StartIn = startIn;
    }

    private void SetEndIn(DateTime endIn)
    {
        if (endIn <= StartIn || endIn < DateTime.Now) throw new ValueOutOfRangeException(nameof(endIn));
        EndIn = endIn;
    }

    private void SetAmount(
        long minAmount,
        long maxAmount)
    {
        if (minAmount > maxAmount) throw new ValueOutOfRangeException(minAmount.ToString(), nameof(minAmount));
        else if (minAmount < 0) throw new ValueOutOfRangeException(minAmount.ToString(), nameof(minAmount));

        if (maxAmount < 0) throw new ValueOutOfRangeException(maxAmount.ToString(), nameof(maxAmount));

        MinAmount = new AmountValueObject(minAmount);
        MaxAmount = new AmountValueObject(maxAmount);
    }

    public void AddBidDetail(BidDetails bidDetail)
    {
        if (bidDetail.Amount.Value < MinAmount.Value || bidDetail.Amount.Value > MaxAmount.Value)
            throw new ValueOutOfRangeException(bidDetail.Amount.ToString(), nameof(bidDetail.Amount));

        var bidDetailInList = BidDetails.FirstOrDefault(_ => _.Id == bidDetail.Id);
        if (bidDetailInList is not null) BidDetails.Remove(bidDetailInList);

        BidDetails.Add(bidDetail);
    }

    public bool IsActive()
    {
        var currentDate = DateTime.Now;
        return currentDate >= StartIn && currentDate <= EndIn;
    }

    public bool IsAccomplished()
    {
        var currentDate = DateTime.Now;
        return currentDate >= StartIn && currentDate >= EndIn;
    }

    public bool HasWinner()
    {
        return BidDetails.Any(_ => _.IsWinner);
    }

    public bool IsAmountInRange(long amount) => amount >= MinAmount.Value && amount <= MaxAmount.Value;
}
