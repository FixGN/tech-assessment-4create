using System.Net.Mail;
using Model = _4Create.Infrastructure.Entities;
using _4Create.Domain.Aggregates.Employees;
using _4Create.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Infrastructure.Repositories.MySql;

public class EmployeesMySqlReadRepository : IEmployeesReadRepository
{
    private readonly MySqlDbContext _dbContext;
    
    public EmployeesMySqlReadRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Employee>> GetByIds(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.CompanyEmployees)
            .Where(e => ids.Contains(e.Id))
            .Select(e => MapToDomainModel(e))
            .ToListAsync(cancellationToken);
    }

    public Task<List<Employee>> GetByCompanyIds(List<Guid> companyIds, CancellationToken cancellationToken)
    {
        return _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.CompanyEmployees)
            .Where(e => e.CompanyEmployees.Any(c => companyIds.Contains(c.CompanyId)))
            .Distinct()
            .Select(e => MapToDomainModel(e))
            .ToListAsync(cancellationToken);
    }

    public Task<bool> IsUserWithEmailExistAsync(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Employees.AnyAsync(e => e.Email == email, cancellationToken);
    }

    private static Employee MapToDomainModel(Model.Employee employee)
    {
        return Employee.Restore(
            employee.Id,
            employee.Title,
            new MailAddress(employee.Email),
            employee.CompanyEmployees.Select(c => c.CompanyId).ToList(),
            employee.CreatedAt,
            employee.CreatedByUserId);
    }
}
