using _4Create.Domain.Interfaces;
using _4Create.Infrastructure.Entities;
using Employee = _4Create.Domain.Aggregates.Employees.Employee;

namespace _4Create.Infrastructure.Repositories.MySql;

public class EmployeesMySqlWriteRepository : IEmployeesWriteRepository
{
    private readonly MySqlDbContext _dbContext;
    
    public EmployeesMySqlWriteRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _dbContext.Employees.AddAsync(
            new Entities.Employee
            {
                Id = employee.Id,
                Title = employee.Title,
                Email = employee.Email.Address,
                CompanyEmployees = employee.CompanyIds.
                    Select(cId => new CompanyEmployee
                    {
                        CompanyId = cId,
                        EmployeeId = employee.Id
                    })
                    .ToList(),
                CreatedAt = employee.CreatedAt,
                CreatedByUserId = employee.CreatedByUserId
            },
            cancellationToken);

        await _dbContext.SystemLogs.AddAsync(
            new SystemLog
            {
                Id = Guid.NewGuid(),
                ResourceType = nameof(Employee),
                ChangeSet = employee,
                CreatedAt = employee.CreatedAt,
                ResourceId = employee.Id,
                EventName = Constants.EventNames.CreateEventName,
                Comment = $"Employee with email {employee.Email} was created"
            },
            cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
