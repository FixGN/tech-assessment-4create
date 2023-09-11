using _4Create.Domain.Aggregates.Employees.Enum;

namespace _4Create.Infrastructure.Entities;

public class Employee
{
    public Guid Id { get; set; } 
    public EmployeeTitle Title { get; set; }
    public string Email { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public List<CompanyEmployee> CompanyEmployees { get; set; } = null!;
    public List<Company> Companies { get; set; } = null!;

}
