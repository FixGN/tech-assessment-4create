using _4Create.Application.Exceptions;
using _4Create.Contracts;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Application;

public class UserPasswordIsInvalidExceptionHandler : IExceptionHandler<UserPasswordIsInvalidException>
{
    private readonly ILogger<CompaniesNotFoundExceptionHandler> _logger;

    public UserPasswordIsInvalidExceptionHandler(ILogger<CompaniesNotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Code = ExceptionCodes.UserPasswordIsInvalidError,
            Message = context.Exception.Message
        };

        _logger.LogInformation(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
