using MediatR;
using Tender.App.Domain.Events;

namespace Tender.App.Application.EventHandlers;

public sealed class BidDetailAddedEventHandler : INotificationHandler<BidDetailAddedEvent>
{
    public async Task Handle(BidDetailAddedEvent notification, CancellationToken cancellationToken)
    {
        Console.Out.WriteLine($"{nameof(BidDetailAddedEvent)} has been fired!");
        await Task.CompletedTask;
    }
}
