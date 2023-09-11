using _4Create.Domain.Aggregates.Companies;

namespace _4Create.Domain.Interfaces;

public interface ICompaniesReadRepository
{
    Task<List<Company>> GetByIdsAsync(
        List<Guid> ids,
        CancellationToken cancellationToken = default);
    
    Task<bool> AreExistByIdsAsync(
        List<Guid> ids,
        CancellationToken cancellationToken = default);

    Task<bool> IsCompanyNameExistedAsync(
        string name,
        CancellationToken cancellationToken = default);
}
