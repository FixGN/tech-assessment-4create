using _4Create.Domain.Aggregates.Employees.Enum;

namespace _4Create.Application.Dtos.Employees.CreateEmployee;

public record CreateEmployeeDto(
    string Email,
    EmployeeTitle Title,
    List<Guid> CompanyIds);
