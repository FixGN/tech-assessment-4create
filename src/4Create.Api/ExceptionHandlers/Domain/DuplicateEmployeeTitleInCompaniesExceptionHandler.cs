using _4Create.Contracts;
using _4Create.Domain.Exceptions;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Domain;

public class DuplicateEmployeeTitleInCompaniesExceptionHandler : IExceptionHandler<DuplicateEmployeeTitleInCompaniesException>
{
    private readonly ILogger<DuplicateEmployeeTitleInCompaniesExceptionHandler> _logger;

    public DuplicateEmployeeTitleInCompaniesExceptionHandler(ILogger<DuplicateEmployeeTitleInCompaniesExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as DuplicateEmployeeTitleInCompaniesException;
        
        var errorResponse = new
        {
            Code = ExceptionCodes.DuplicateEmployeeTitleInCompaniesError,
            Message = exception!.Message,
            Title = exception!.Title,
            CompanyIds = exception!.CompanyIds
        };

        _logger.LogInformation(exception!.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
