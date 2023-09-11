using _4Create.Domain.Aggregates.Employees;

namespace _4Create.Domain.Interfaces;

public interface IEmployeesWriteRepository
{
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
