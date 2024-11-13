using MediatR;
using Tender.App.Domain.Events;

namespace Tender.App.Application.EventHandlers;

public sealed class AddBidEventHandler : INotificationHandler<BidAddedEvent>
{
    public async Task Handle(BidAddedEvent notification, CancellationToken cancellationToken)
    {
        Console.Out.WriteLine($"{nameof(BidAddedEvent)} has been fired!");
        await Task.CompletedTask;
    }
}
