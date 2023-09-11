using System.Net.Mail;
using _4Create.Domain.Aggregates.Employees.Enum;
using _4Create.Domain.DomainEvents.Employees;

namespace _4Create.Domain.Aggregates.Employees;

public class Employee : AggregateRoot
{
    private readonly List<Guid> _companyIds;

    private Employee(
        Guid id, 
        EmployeeTitle title,
        MailAddress email,
        List<Guid> companyIds,
        DateTimeOffset createdAt,
        Guid createdByUserId) : base(id)
    {
        Title = title;
        Email = email;
        _companyIds = companyIds;
        CreatedAt = createdAt;
        CreatedByUserId = createdByUserId;
    }
    
    public EmployeeTitle Title { get; private set; }

    public MailAddress Email { get; private set; }

    public IReadOnlyCollection<Guid> CompanyIds => _companyIds.AsReadOnly();

    public DateTimeOffset CreatedAt { get; private set; }

    public Guid CreatedByUserId { get; private set; }
    
    public static Employee Create(
        EmployeeTitle title,
        MailAddress email,
        List<Guid> companyIds,
        Guid createdByUserId)
    {
        var employee = new Employee(
            Guid.NewGuid(),
            title,
            email,
            companyIds,
            DateTimeOffset.UtcNow,
            createdByUserId);

        employee.RaiseDomainEvent(EmployeeCreatedEvent.Create(employee));

        return employee;
    }
    
    public static Employee Restore(
        Guid id,
        EmployeeTitle title,
        MailAddress email,
        List<Guid> companyIds,
        DateTimeOffset createdAt,
        Guid createdByUserId)
    {
        return new Employee(
            id,
            title,
            email,
            companyIds,
            createdAt,
            createdByUserId);
    }
}
