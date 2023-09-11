namespace _4Create.Application.Exceptions;

public class CompaniesNotFoundException : Exception
{
    public CompaniesNotFoundException(
        List<Guid> companyIds) : base($"Companies with IDs {string.Join(',', companyIds)} not found!")
    {
        CompanyIds = companyIds;
    }
    
    public List<Guid> CompanyIds { get; }
}
