namespace _4Create.Application.Exceptions;

public class EmployeesNotFoundException : Exception
{
    public EmployeesNotFoundException(
        IReadOnlyCollection<Guid> employeeIds) : base($"Employees with Ids {string.Join(',', employeeIds)} not found!")
    {
        EmployeeIds = employeeIds;
    }
    
    public IReadOnlyCollection<Guid> EmployeeIds { get; init; }
}
