namespace _4Create.Domain.Exceptions;

public class CompanyNameExistsException : Exception
{
    public CompanyNameExistsException() : base($"Company with same name already exists!")
    { }
}
