namespace _4Create.Domain.Exceptions;

public class CompaniesDoesntExistException : Exception
{
    public CompaniesDoesntExistException(
        IReadOnlyCollection<Guid> nonexistentCompanyIds) : base($"You can't add employee to nonexistent companies!")
    {
        this.NonexistentCompanyIds = nonexistentCompanyIds;
    }
    
    public IReadOnlyCollection<Guid> NonexistentCompanyIds { get; }
}
