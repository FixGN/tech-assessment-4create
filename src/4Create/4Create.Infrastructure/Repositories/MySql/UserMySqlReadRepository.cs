using _4Create.Domain.Aggregates.Users;
using _4Create.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Infrastructure.Repositories.MySql;

public class UserMySqlReadRepository : IUserReadRepository
{
    private readonly MySqlDbContext _dbContext;
    
    public UserMySqlReadRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<User?> GetByName(string username, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Where(u => u.Username == username)
            .Select(u =>
                new User(
                    u.Id,
                    u.Username,
                    u.HashedPassword,
                    u.Role,
                    u.CreatedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
