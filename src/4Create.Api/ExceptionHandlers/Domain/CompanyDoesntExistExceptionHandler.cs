using _4Create.Contracts;
using _4Create.Domain.Exceptions;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Domain;

public class CompanyDoesntExistExceptionHandler : IExceptionHandler<CompaniesDoesntExistException>
{
    private readonly ILogger<CompanyDoesntExistExceptionHandler> _logger;

    public CompanyDoesntExistExceptionHandler(ILogger<CompanyDoesntExistExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as CompaniesDoesntExistException;
        
        var errorResponse = new
        {
            Code = ExceptionCodes.CompanyDoesntExistError,
            Message = exception!.Message,
            NonexistentCompanyIds = exception.NonexistentCompanyIds
        };

        _logger.LogInformation(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
