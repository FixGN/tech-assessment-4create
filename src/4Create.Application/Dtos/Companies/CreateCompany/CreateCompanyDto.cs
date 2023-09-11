namespace _4Create.Application.Dtos.Companies.CreateCompany;

public record CreateCompanyDto(
    string Name,
    List<CreateCompanyDtoEmployee> Employees);
