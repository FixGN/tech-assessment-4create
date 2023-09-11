using _4Create.Application.Exceptions;
using _4Create.Contracts;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Application;

public class CompaniesNotFoundExceptionHandler : IExceptionHandler<CompaniesNotFoundException>
{
    private readonly ILogger<CompaniesNotFoundExceptionHandler> _logger;

    public CompaniesNotFoundExceptionHandler(ILogger<CompaniesNotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as CompaniesNotFoundException;
        
        var errorResponse = new
        {
            Code = ExceptionCodes.CompanyNotFoundError,
            Message = exception!.Message,
            CompanyIds = exception.CompanyIds
        };

        _logger.LogInformation(exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
