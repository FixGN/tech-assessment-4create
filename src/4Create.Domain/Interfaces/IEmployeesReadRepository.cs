using _4Create.Domain.Aggregates.Employees;

namespace _4Create.Domain.Interfaces;

public interface IEmployeesReadRepository
{
    Task<List<Employee>> GetByIds(List<Guid> ids, CancellationToken cancellationToken = default);
    Task<List<Employee>> GetByCompanyIds(List<Guid> companyIds, CancellationToken cancellationToken = default);
    Task<bool> IsUserWithEmailExistAsync(string email, CancellationToken cancellationToken = default);
}
