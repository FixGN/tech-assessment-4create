using _4Create.Application.UseCases.Employees.Commands.CreateEmployee;
using _4Create.Contracts.Dtos.Employees.CreateEmployee.Request;
using _4Create.Contracts.Dtos.Employees.CreateEmployee.Response;
using _4Create.WebApi.Authentication.Extensions;
using _4Create.WebApi.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("/api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create new employee
    /// </summary>
    /// <param name="request">Employee data</param>
    /// <returns>Employee Id</returns>
    [HttpPost]
    public async Task<ActionResult<CreateEmployeeResponse>> Get([FromBody] CreateEmployeeRequest request)
    {
        var userId = User.GetUserId();
        
        var command = new CreateEmployeeCommand(
            userId,
            EmployeesMapper.CreateEmployeeRequestToDto(request));

        var employee = await _mediator.Send(command);

        return Ok(new CreateEmployeeResponse(employee.Id.ToString()));
    }
}
