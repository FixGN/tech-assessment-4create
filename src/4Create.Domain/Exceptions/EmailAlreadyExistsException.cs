namespace _4Create.Domain.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException(string email) : base($"Employee with email {email} already exists!")
    {
        email = Email;
    }

    public string Email { get; } = null!;
}
