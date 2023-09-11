using _4Create.Domain.Aggregates.Users.Enums;

namespace _4Create.Domain.Aggregates.Users;

public class User
{
    public User(
        Guid id,
        string username,
        string password,
        UserRole role,
        DateTimeOffset createdAt)
    {
        Id = id;
        Username = username;
        HashedPassword = password;
        Role = role;
        CreatedAt = createdAt;
    }
    
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string HashedPassword { get; private set; }
    public UserRole Role { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    static User Restore(
        Guid id,
        string username,
        string password,
        UserRole role,
        DateTimeOffset createdAt)
    {
        return new User(
            id,
            username,
            password,
            role,
            createdAt);
    }
}
