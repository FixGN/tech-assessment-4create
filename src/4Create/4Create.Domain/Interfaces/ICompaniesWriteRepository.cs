using _4Create.Domain.Aggregates.Companies;

namespace _4Create.Domain.Interfaces;

public interface ICompaniesWriteRepository
{
    Task AddAsync(Company company, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
