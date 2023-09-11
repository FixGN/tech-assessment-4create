namespace _4Create.Contracts.Dtos.Companies.CreateCompany.Request;

public record CreateCompanyRequest(
    string Name,
    List<CreateCompanyRequestEmployee> Employees);
