using _4Create.Domain.Aggregates.Employees;
using _4Create.Domain.Aggregates.Employees.Enum;
using _4Create.Domain.Exceptions;
using _4Create.Domain.Interfaces;
using _4Create.Domain.Services.Abstraction;

namespace _4Create.Domain.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompaniesReadRepository _companiesRepository;

    public CompanyService(ICompaniesReadRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }
    
    public async Task ValidateIsCompanyNameUnique(string name, CancellationToken cancellationToken = default)
    {
        var isNameExisted = await _companiesRepository.IsCompanyNameExistedAsync(name, cancellationToken);

        if (isNameExisted)
        {
            throw new CompanyNameExistsException();
        }
    }

    public void ValidateEmployeesTitles(IReadOnlyCollection<EmployeeTitle> employeeTitles)
    {
        if (employeeTitles.Count != employeeTitles.Distinct().Count())
        {
            throw new DuplicateEmployeeTitleException();
        }
    }
    
    public void ValidateEmployeeTitleAcrossCompanies(
        EmployeeTitle title,
        List<Guid> companyIds,
        List<Employee> employees)
    {
        if (employees.Count is 0)
        {
            return;
        }

        var companiesWithSameTitleEmployees = new HashSet<Guid>();

        foreach (var employee in employees)
        {
            if (employee.Title == title)
            {
                companiesWithSameTitleEmployees.UnionWith(employee.CompanyIds);
            }
        }

        if (companiesWithSameTitleEmployees.Count > 0)
        {
            throw new DuplicateEmployeeTitleInCompaniesException(
                title,
                companiesWithSameTitleEmployees
                    .Where(companyIds.Contains)
                    .ToList());
        }
    }
    
    public async Task ValidateExistenceCompaniesAsync(List<Guid> companyIds, CancellationToken cancellationToken = default)
    {
        var companies = await _companiesRepository.GetByIdsAsync(companyIds, cancellationToken);

        if (companies.Count != companyIds.Count)
        {
            var foundedCompanyIds = companies
                .Select(c => c.Id);
            var unfoundedCompanyIds = companyIds
                .Where(cId => foundedCompanyIds.Contains(cId));

            throw new CompaniesDoesntExistException(unfoundedCompanyIds.ToList());
        }
    }
}
