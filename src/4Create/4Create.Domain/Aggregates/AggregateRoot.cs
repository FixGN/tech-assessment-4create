using _4Create.Domain.DomainEvents;

namespace _4Create.Domain.Aggregates;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvents> _domainEvents = new();

    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {
    }
    
    public Guid Id { get; private init; }

    public IReadOnlyCollection<IDomainEvents> GetDomainEvents() => _domainEvents;

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvents domainEvent) =>
        _domainEvents.Add(domainEvent);
}
