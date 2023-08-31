namespace CostMSWebAPI.Utils;

using CostMSWebAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

public static class ExceptionHandlerHelper
{
    public static Task HandleException(HttpContext httpContext, IExceptionHandlerFeature exceptionHandlerFeature, int statusCode)
    {
        httpContext.Response.StatusCode = statusCode;
        Exception exception = exceptionHandlerFeature.Error;
        return httpContext.Response.WriteAsJsonAsync(
            new ErrorInfo()
            {
                Status = httpContext.Response.StatusCode,
                Url = exceptionHandlerFeature.Path, 
                Message = exception != exception.GetBaseException() 
                        ? exception.Message + " " + exception.GetBaseException().Message
                        : exception.Message
            }
        );
    }
}
