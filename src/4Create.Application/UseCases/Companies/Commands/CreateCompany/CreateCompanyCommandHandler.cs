using _4Create.Application.Dtos.Companies.CreateCompany;
using _4Create.Application.Exceptions;
using _4Create.Domain.Aggregates.Companies;
using _4Create.Domain.Aggregates.Employees;
using _4Create.Domain.Interfaces;
using _4Create.Domain.Services.Abstraction;
using MediatR;
using System.Net.Mail;

namespace _4Create.Application.UseCases.Companies.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyService _domainService;
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeesReadRepository _employeesReadRepository;
    private readonly IEmployeesWriteRepository _employeesWriteRepository;
    private readonly ICompaniesWriteRepository _companiesWriteRepository;

    public CreateCompanyCommandHandler(
        ICompanyService domainService,
        IEmployeesReadRepository employeesReadRepository,
        IEmployeesWriteRepository employeesWriteRepository,
        ICompaniesWriteRepository companiesWriteRepository,
        IEmployeeService employeeService)
    {
        _domainService = domainService;
        _employeesReadRepository = employeesReadRepository;
        _employeesWriteRepository = employeesWriteRepository;
        _companiesWriteRepository = companiesWriteRepository;
        _employeeService = employeeService;
    }
    
    public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        await _domainService.ValidateIsCompanyNameUnique(request.Company.Name, cancellationToken);

        var existedEmployees = await GetExistedEmployees(
            request.Company.Employees,
            request.CreatedById,
            cancellationToken);

        var newEmployees = await GetEmployeeAgregates(
            request.Company.Employees,
            request.CreatedById,
            cancellationToken);

        ValidateTitles(existedEmployees.Concat(newEmployees));

        foreach (var employee in newEmployees)
        {
            await _employeesWriteRepository.AddAsync(employee, cancellationToken);
        }
        await _employeesWriteRepository.SaveChangesAsync(cancellationToken);

        var company = Company.Create(
            request.Company.Name,
            existedEmployees.Concat(newEmployees).Select(e => e.Id).ToList(),
            DateTimeOffset.UtcNow,
            request.CreatedById);

        await _companiesWriteRepository.AddAsync(company, cancellationToken);
        await _companiesWriteRepository.SaveChangesAsync(cancellationToken);

        return company.Id;
    }

    private async Task<List<Employee>> GetEmployeeAgregates(
        List<CreateCompanyDtoEmployee> requestEmployees,
        Guid createdBy,
        CancellationToken cancellationToken)
    {
        var newEmployees = requestEmployees
            .Where(e => e.Id is null)
            .ToList();

        var employees = new List<Employee>(newEmployees.Count);
        foreach (var newEmployee in newEmployees)
        {
            var email = new MailAddress(newEmployee.Email!);
            await _employeeService.ValidateEmailUniquenessAsync(email, cancellationToken);
            var employee = Employee.Create(
                newEmployee.Title!.Value,
                email,
                new List<Guid>(),
                createdBy);
            employees.Add(employee);
        }
        return employees;
    }

    private void ValidateTitles(IEnumerable<Employee> employees)
    {
        var employeeTitles = employees.Select(e => e.Title).ToList();
        _domainService.ValidateEmployeesTitles(employeeTitles);
    }

    
    private async Task<List<Employee>> GetExistedEmployees(
        IEnumerable<CreateCompanyDtoEmployee> employees,
        Guid createdBy,
        CancellationToken cancellationToken = default)
    {
        var employeeIdsForRequest = employees
            .Where(e => e.Id is not null)
            .Select(e => e.Id!.Value)
            .ToList();

        if (employeeIdsForRequest.Count > 0)
        {
            var existedEmployee = await _employeesReadRepository
                .GetByIds(employeeIdsForRequest, cancellationToken);

            if (existedEmployee.Count != employeeIdsForRequest.Count)
            {
                var existedEmployeeIds = existedEmployee
                    .Select(e => e.Id)
                    .ToList();

                var unfoundedEmployeeGuids = employeeIdsForRequest
                    .Where(eId => !existedEmployeeIds.Contains(eId))
                    .ToList();
                
                throw new EmployeesNotFoundException(unfoundedEmployeeGuids);
            }

            return existedEmployee;
        }

        return new List<Employee>();
    }
}
