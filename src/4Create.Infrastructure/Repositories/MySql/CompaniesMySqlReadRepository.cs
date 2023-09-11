using _4Create.Domain.Aggregates.Companies;
using _4Create.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Infrastructure.Repositories.MySql;

public class CompaniesMySqlReadRepository : ICompaniesReadRepository
{
    private readonly MySqlDbContext _dbContext;
    
    public CompaniesMySqlReadRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<Company>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _dbContext.Companies
            .AsNoTracking()
            .Include(c => c.CompanyEmployees)
            .Where(c => ids.Contains(c.Id))
            .Select(c => Company.Restore(
                c.Id,
                c.Name,
                c.CompanyEmployees.Select(e => e.EmployeeId).ToList(),
                c.CreatedAt,
                c.CreatedByUserId))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AreExistByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        var existsCompaniesCount = await _dbContext.Companies
            .CountAsync(c => ids.Contains(c.Id), cancellationToken);
        return existsCompaniesCount == ids.Count;
    }

    public Task<bool> IsCompanyNameExistedAsync(string name, CancellationToken cancellationToken = default)
    {
        return _dbContext.Companies
            .AnyAsync(c => c.Name == name, cancellationToken);
    }
}
