using _4Create.Domain.Aggregates.Employees;
using _4Create.Domain.Aggregates.Employees.Enum;

namespace _4Create.Domain.Services.Abstraction;

public interface ICompanyService
{
    Task ValidateIsCompanyNameUnique(string name, CancellationToken cancellationToken = default);

    void ValidateEmployeesTitles(IReadOnlyCollection<EmployeeTitle> employeeTitles);
    
    Task ValidateExistenceCompaniesAsync(
        List<Guid> companyIds,
        CancellationToken cancellationToken = default);
    
    void ValidateEmployeeTitleAcrossCompanies(
        EmployeeTitle title,
        List<Guid> companyIds,
        List<Employee> employees);
}
