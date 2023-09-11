using _4Create.Domain.Aggregates.Employees.Enum;

namespace _4Create.Domain.Exceptions;

public class DuplicateEmployeeTitleInCompaniesException : Exception
{
    public DuplicateEmployeeTitleInCompaniesException(
        EmployeeTitle title,
        List<Guid> companyIds) : base($"Employee with title {title} already exists in companies with Ids {string.Join(',', companyIds)}")
    {
        Title = title.ToString();
        CompanyIds = companyIds;
    }

    public string Title { get; }
    public List<Guid> CompanyIds { get; }
}
