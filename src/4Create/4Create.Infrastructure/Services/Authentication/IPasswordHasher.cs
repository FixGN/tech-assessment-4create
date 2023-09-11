namespace _4Create.Infrastructure.Services.Authentication
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string hashedPassword, string password);
    }
}
