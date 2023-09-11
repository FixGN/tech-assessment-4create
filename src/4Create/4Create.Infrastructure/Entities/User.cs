using _4Create.Domain.Aggregates.Users.Enums;

namespace _4Create.Infrastructure.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
