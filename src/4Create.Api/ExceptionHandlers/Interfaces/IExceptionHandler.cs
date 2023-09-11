using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.ExceptionHandlers.Interfaces;

public interface IExceptionHandler
{
    void OnException(ExceptionContext context);
}

public interface IExceptionHandler<T> : IExceptionHandler where T : Exception
{
}
