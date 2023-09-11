namespace _4Create.Domain.Exceptions;

public class DuplicateEmployeeTitleException : Exception
{
    public DuplicateEmployeeTitleException() : base($"You can't add more than 1 employee with same title to the company!")
    { }
}
