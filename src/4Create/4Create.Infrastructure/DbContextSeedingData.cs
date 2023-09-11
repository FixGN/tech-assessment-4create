using _4Create.Domain.Aggregates.Users.Enums;
using _4Create.Infrastructure.Entities;

namespace _4Create.Infrastructure;

public static class DbContextSeedingData
{
    public static IEnumerable<User> GetUsers()
    {
        return new List<User>
        {
            new()
            {
                Id = new Guid("2E09AB24-262F-4F1A-A042-30CC88D71D80"),
                Username = "Root",
                HashedPassword = "$argon2id$v=19$m=65536,t=3,p=1$1DOQgzyfEfZ2i/34yhW1oA$P5MoPcXcQoLvdOXfQtzwMM+c189Jm0cIAgcBzo7kJKE",
                Role = UserRole.Admin,
                CreatedAt = new DateTimeOffset(2023, 09, 10, 00, 00, 00, TimeSpan.Zero)
            }
        };
    }
}
