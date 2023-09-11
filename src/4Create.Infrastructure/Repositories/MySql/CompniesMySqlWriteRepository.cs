using _4Create.Domain.Interfaces;
using _4Create.Infrastructure.Entities;
using Company = _4Create.Domain.Aggregates.Companies.Company;

namespace _4Create.Infrastructure.Repositories.MySql;

public class CompaniesMySqlWriteRepository : ICompaniesWriteRepository
{
    private readonly MySqlDbContext _dbContext;
    
    public CompaniesMySqlWriteRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
    {
        await _dbContext.Companies.AddAsync(
            new Entities.Company
            {
                Id = company.Id,
                Name = company.Name,
                CompanyEmployees = company.EmployeeIds
                    .Select(eId => new CompanyEmployee
                    {
                        CompanyId = company.Id,
                        EmployeeId = eId
                    })
                    .ToList(),
                CreatedAt = company.CreatedAt,
                CreatedByUserId = company.CreatedByUserId
            },
            cancellationToken);

        await _dbContext.SystemLogs.AddAsync(
            new SystemLog
            {
                Id = Guid.NewGuid(),
                ResourceType = nameof(Company),
                ChangeSet = company,
                CreatedAt = company.CreatedAt,
                ResourceId = company.Id,
                EventName = Constants.EventNames.CreateEventName,
                Comment = $"Company with name {company.Name} was created"
            },
            cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
