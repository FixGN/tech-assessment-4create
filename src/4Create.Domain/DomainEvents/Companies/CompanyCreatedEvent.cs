using _4Create.Domain.Aggregates.Companies;

namespace _4Create.Domain.DomainEvents.Companies;

public class CompanyCreatedEvent : IDomainEvents
{
    private CompanyCreatedEvent(
        Guid aggregateId,
        string aggregateType,
        DateTimeOffset createdAt,
        Company changeSet,
        string comment)
    {
        AggregateId = aggregateId;
        AggregateType = aggregateType;
        CreatedAt = createdAt;
        ChangeSet = changeSet;
        Comment = comment;

    }

    public Guid AggregateId { get; }
    public string AggregateType { get; }
    public DateTimeOffset CreatedAt { get; }
    public object ChangeSet { get; }
    public string Comment { get; }

    public static CompanyCreatedEvent Create(Company changeSet)
    {
        return new(
            changeSet.Id,
            nameof(Company),
            changeSet.CreatedAt,
            changeSet,
            $"Company '{changeSet.Name}' was created");
    }
}
