using _4Create.Application.Exceptions;
using _4Create.Contracts;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Application;

public class UserNotFoundExceptionHandler : IExceptionHandler<UserNotFoundException>
{
    private readonly ILogger<CompaniesNotFoundExceptionHandler> _logger;

    public UserNotFoundExceptionHandler(ILogger<CompaniesNotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Code = ExceptionCodes.UserNotFoundError,
            Message = context.Exception.Message
        };

        _logger.LogInformation(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
