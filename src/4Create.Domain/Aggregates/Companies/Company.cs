using _4Create.Domain.DomainEvents.Companies;

namespace _4Create.Domain.Aggregates.Companies;

public class Company : AggregateRoot
{
    private readonly List<Guid> _employeeIds;

    private Company(
        Guid id,
        string name,
        List<Guid> employeeIds,
        DateTimeOffset createdAt,
        Guid createdByUserId) : base(id)
    {
        Name = name;
        _employeeIds = employeeIds;
        CreatedAt = createdAt;
        CreatedByUserId = createdByUserId;
    }

    public string Name { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public IReadOnlyCollection<Guid> EmployeeIds => _employeeIds;

    public static Company Create(string name, List<Guid> employeeIds, DateTimeOffset createdAt, Guid createdByUserId)
    {
        var companyId = Guid.NewGuid();
        
        var company = new Company(
            companyId,
            name,
            employeeIds,
            DateTimeOffset.UtcNow,
            createdByUserId);

        company.RaiseDomainEvent(
            CompanyCreatedEvent.Create(company));

        return company;
    }
    
    public static Company Restore(Guid id, string name, List<Guid> employeeIds, DateTimeOffset createdAt, Guid createdByUserId)
    {
        var company = new Company(
            id,
            name,
            employeeIds,
            createdAt,
            createdByUserId);

        company.RaiseDomainEvent(
            CompanyCreatedEvent.Create(company));

        return company;
    }
}
