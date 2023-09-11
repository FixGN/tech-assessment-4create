using _4Create.Domain.Aggregates.Users.Enums;

namespace _4Create.Infrastructure.Services.Authentication;

public interface IJwtTokenService
{
    string GenerateJwtToken(Guid id, string username, UserRole role);
}
