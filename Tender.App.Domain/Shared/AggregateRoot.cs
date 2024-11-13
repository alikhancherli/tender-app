using System.ComponentModel.DataAnnotations.Schema;

namespace Tender.App.Domain.Shared;

public abstract class AggregateRoot<TId> : Entity<TId>, IDomainEvent where TId : IComparable<TId>
{
    public int Version { get; protected set; }

    private bool _versionIncremented;

    [NotMapped]
    public IReadOnlyList<IDomainEventFlag> Events => _events;

    private readonly List<IDomainEventFlag> _events = new();


    protected void AddEvent(IDomainEventFlag @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }

        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();

}
