using _4Create.Contracts;
using _4Create.Domain.Exceptions;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Domain;

public class EmailAlreadyExistsExceptionHandler : IExceptionHandler<EmailAlreadyExistsException>
{
    private readonly ILogger<EmailAlreadyExistsExceptionHandler> _logger;

    public EmailAlreadyExistsExceptionHandler(ILogger<EmailAlreadyExistsExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception as EmailAlreadyExistsException;
        
        var errorResponse = new
        {
            Code = ExceptionCodes.EmailAlreadyExistsError,
            Message = exception!.Message,
            Email = exception.Email
        };

        _logger.LogInformation(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
