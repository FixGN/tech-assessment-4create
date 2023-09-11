using _4Create.Contracts.Dtos.Enums;

namespace _4Create.Contracts.Dtos.Employees.CreateEmployee.Request;

public record CreateEmployeeRequest(
    string Email,
    EmployeeTitle Title,
    List<string> CompanyIds);
