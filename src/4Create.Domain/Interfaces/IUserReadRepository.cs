using _4Create.Domain.Aggregates.Users;

namespace _4Create.Domain.Interfaces;

public interface IUserReadRepository
{
    Task<User?> GetByName(string username, CancellationToken cancellationToken = default);
}
