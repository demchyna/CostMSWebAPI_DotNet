using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Utils;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case DataNotFoundException _: 
                    await HandleException(httpContext, exception, 404);
                    break;
                case NoDataException _:
                    await HandleException(httpContext, exception, 417);
                    break;
                case ArgumentNullException _:
                    await HandleException(httpContext, exception, 418);
                    break;
                case IncorrectDataException _:
                    await HandleException(httpContext, exception, 422);
                    break;
                case ConflictDataException _:
                    await HandleException(httpContext, exception, 409);
                    break;
                case not null:
                    await HandleException(httpContext, exception, 400);
                    break;
            }
        }
    }
        
    private async Task HandleException(HttpContext httpContext, Exception exception, int statusCode)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsJsonAsync(
            new ErrorInfo()
            {
                Status = httpContext.Response.StatusCode,
                Url = httpContext.Request.Path, 
                Message = exception != exception.GetBaseException() 
                    ? exception.Message + " " + exception.GetBaseException().Message
                    : exception.Message
            });
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}