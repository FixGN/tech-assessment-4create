namespace _4Create.Application.Exceptions;

public class UserPasswordIsInvalidException : Exception
{
    public UserPasswordIsInvalidException() : base($"User password is invalid!")
    { }
}
