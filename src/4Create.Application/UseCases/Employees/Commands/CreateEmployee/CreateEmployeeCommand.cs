using _4Create.Application.Dtos.Employees.CreateEmployee;
using _4Create.Domain.Aggregates.Employees;
using MediatR;

namespace _4Create.Application.UseCases.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(
    Guid CreatedById,
    CreateEmployeeDto Employee) : IRequest<Employee>;
