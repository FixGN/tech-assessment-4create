namespace _4Create.Infrastructure.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public List<CompanyEmployee> CompanyEmployees { get; set; } = null!;
    public List<Employee> Employees { get; set; } = null!;
}
