using _4Create.Contracts.Dtos.Enums;

namespace _4Create.Contracts.Dtos.Companies.CreateCompany.Request;

public record CreateCompanyRequestEmployee(
    string? Email,
    EmployeeTitle? Title,
    Guid? Id);
