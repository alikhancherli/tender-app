using MediatR;

namespace Tender.App.Domain.Shared;

public interface IDomainEvent : INotification
{
    public IReadOnlyList<IDomainEventFlag> Events { get; }
    void ClearEvents();
}
