using _4Create.Domain.Aggregates.Companies;

namespace _4Create.Domain.DomainEvents.Companies;

public sealed class CompanyHiredNewEmployeeEvent : IDomainEvents
{
    private CompanyHiredNewEmployeeEvent(
        Guid aggregateId,
        DateTimeOffset createdAt,
        Company changeSet)
    {
        AggregateId = aggregateId;
        CreatedAt = createdAt;
        ChangeSet = changeSet;
    }
    
    public Guid AggregateId { get; }
    public DateTimeOffset CreatedAt { get; }
    public object ChangeSet { get; }

    public static CompanyHiredNewEmployeeEvent Create(Company company)
    {
        return new CompanyHiredNewEmployeeEvent(
            company.Id,
            company.CreatedAt,
            company);
    }
}
