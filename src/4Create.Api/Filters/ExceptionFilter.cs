using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4Create.WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exceptionType = context.Exception.GetType();

        while (exceptionType != typeof(Exception))
        {
            var genericInterface = typeof(IExceptionHandler<>).MakeGenericType(exceptionType!);

            if (context.HttpContext.RequestServices.GetService(genericInterface) is IExceptionHandler handler)
            {
                handler.OnException(context);
            }

            exceptionType = exceptionType!.BaseType;
        }

        var defaultHandler = context.HttpContext.RequestServices.GetService<IExceptionHandler>();
        defaultHandler!.OnException(context);
    }
}
