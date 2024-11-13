using Tender.App.Domain.Shared;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Domain.Entities;

public sealed class BidDetails : Entity<int>
{
    public int UserId { get; private set; }
    public AmountValueObject Amount { get; private set; }
    public bool IsWinner { get; private set; }
    
    private BidDetails() { }
    public BidDetails(
        int userId,
        long amount)
    {
        UserId = userId;
        Amount = new AmountValueObject(amount);
        IsWinner = false;
        CreatedTimeUtc= DateTime.UtcNow;
    }

    public void SetWinner()
    {
        IsWinner = true;
    }
}
