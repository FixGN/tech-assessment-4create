using System.Net.Mail;
using _4Create.Application.Exceptions;
using _4Create.Domain.Aggregates.Companies;
using _4Create.Domain.Aggregates.Employees;
using _4Create.Domain.Interfaces;
using _4Create.Domain.Services.Abstraction;
using MediatR;

namespace _4Create.Application.UseCases.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IEmployeeService _employeeService;
    private readonly ICompanyService _companyService;
    private readonly IEmployeesWriteRepository _employeesWriteRepository;
    private readonly IEmployeesReadRepository _employeesReadRepository;
    private readonly ICompaniesReadRepository _companiesRepository;

    public CreateEmployeeCommandHandler(
        IEmployeeService employeeService,
        ICompanyService companyService,
        IEmployeesReadRepository employeesReadRepository,
        IEmployeesWriteRepository employeesWriteRepository,
        ICompaniesReadRepository companiesRepository)
    {
        _employeeService = employeeService;
        _employeesReadRepository = employeesReadRepository;
        _employeesWriteRepository = employeesWriteRepository;
        _companiesRepository = companiesRepository;
        _companyService = companyService;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var companies = await GetCompanies(request.Employee.CompanyIds, cancellationToken);
        var actualEmployees = await GetActualEmployees(request.Employee.CompanyIds, cancellationToken);

        var email = new MailAddress(request.Employee.Email);

        await _employeeService.ValidateEmailUniquenessAsync(
            email,
            cancellationToken);
        await _companyService.ValidateExistenceCompaniesAsync(
            request.Employee.CompanyIds,
            cancellationToken);
        _companyService.ValidateEmployeeTitleAcrossCompanies(
            request.Employee.Title,
            request.Employee.CompanyIds,
            actualEmployees);

        var employee = Employee.Create(
            request.Employee.Title,
            email,
            companies.Select(c => c.Id).ToList(),
            request.CreatedById);

        await _employeesWriteRepository.AddAsync(employee, cancellationToken);
        await _employeesWriteRepository.SaveChangesAsync(cancellationToken);

        return employee;
    }

    private async Task<List<Company>> GetCompanies(
        List<Guid> companyIds,
        CancellationToken cancellationToken = default)
    {
        var companies = await _companiesRepository.GetByIdsAsync(
            companyIds,
            cancellationToken);
        
        if (companies.Count != companyIds.Count)
        {
            var foundCompanyIds = companies
                .Select(c => c.Id)
                .ToList();
            var unfoundedCompanyIds = companyIds
                .Where(cId => !foundCompanyIds.Contains(cId))
                .ToList();

            throw new CompaniesNotFoundException(unfoundedCompanyIds);
        }

        return companies;
    }
    
    private async Task<List<Employee>> GetActualEmployees(
        List<Guid> companyIds,
        CancellationToken cancellationToken = default)
    {
        var actualEmployees = await _employeesReadRepository.GetByCompanyIds(
            companyIds,
            cancellationToken);
        return actualEmployees.ToList();
    }
}
