using _4Create.Application.Dtos.Employees.CreateEmployee;
using _4Create.Contracts.Dtos.Employees.CreateEmployee.Request;
using Riok.Mapperly.Abstractions;

namespace _4Create.WebApi.Mappers;

[Mapper]
public static partial class EmployeesMapper
{
    public static partial CreateEmployeeDto CreateEmployeeRequestToDto(CreateEmployeeRequest request);
}
