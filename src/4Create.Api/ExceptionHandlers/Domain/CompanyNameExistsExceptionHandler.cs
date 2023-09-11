using _4Create.Contracts;
using _4Create.Domain.Exceptions;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Domain;

public class CompanyNameExistsExceptionHandler : IExceptionHandler<CompanyNameExistsException>
{
    private readonly ILogger<CompanyNameExistsExceptionHandler> _logger;

    public CompanyNameExistsExceptionHandler(ILogger<CompanyNameExistsExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Code = ExceptionCodes.CompanyNameExistsError,
            Message = context.Exception.Message
        };

        _logger.LogInformation(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
