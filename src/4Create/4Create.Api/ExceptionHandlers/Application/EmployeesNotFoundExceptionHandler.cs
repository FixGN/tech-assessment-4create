using _4Create.Application.Exceptions;
using _4Create.Contracts;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Application;

public class EmployeesNotFoundExceptionHandler : IExceptionHandler<EmployeesNotFoundException>
{
    private readonly ILogger<CompaniesNotFoundExceptionHandler> _logger;

    public EmployeesNotFoundExceptionHandler(ILogger<CompaniesNotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as EmployeesNotFoundException;
        
        var errorResponse = new
        {
            Code = ExceptionCodes.EmployeesNotFoundError,
            Message = exception!.Message,
            EmployeeIds = exception.EmployeeIds
        };

        _logger.LogInformation(exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
