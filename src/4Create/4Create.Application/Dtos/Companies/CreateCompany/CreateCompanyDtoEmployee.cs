using _4Create.Domain.Aggregates.Employees.Enum;

namespace _4Create.Application.Dtos.Companies.CreateCompany;

public record CreateCompanyDtoEmployee(
    Guid? Id,
    string? Email,
    EmployeeTitle? Title);
