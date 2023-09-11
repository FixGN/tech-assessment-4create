using _4Create.Domain.Aggregates.Employees;

namespace _4Create.Domain.DomainEvents.Employees;

public sealed class EmployeeCreatedEvent : IDomainEvents
{
    private EmployeeCreatedEvent(
        Guid aggregateId,
        DateTimeOffset createdAt,
        Employee changeSet)
    {
        AggregateId = aggregateId;
        CreatedAt = createdAt;
        ChangeSet = changeSet;
    }
    
    public Guid AggregateId { get; }
    public DateTimeOffset CreatedAt { get; }
    public object ChangeSet { get; }

    public static EmployeeCreatedEvent Create(Employee employee)
    {
        return new EmployeeCreatedEvent(
            employee.Id,
            employee.CreatedAt,
            employee);
    }
}
