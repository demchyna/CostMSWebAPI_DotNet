using CostMSWebAPI.Utils;
using Microsoft.AspNetCore.Diagnostics;

namespace CostMSWebAPI.Exceptions;

public static class ApplicationExceptionHandler
{
    public static IApplicationBuilder HandleExceptions(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.Run(async httpContext =>
        {
            httpContext.Response.ContentType = "application/json";
            
            IExceptionHandlerFeature? exceptionHandlerFeature = httpContext.Features
                    .Get<IExceptionHandlerFeature>();

            Console.WriteLine("Exception type : " + exceptionHandlerFeature?.Error);

            if (exceptionHandlerFeature?.Error is DataNotFoundException)
            {
                await ExceptionHandlerHelper.HandleException(httpContext, exceptionHandlerFeature, 404);
                // return;        
            }
            if (exceptionHandlerFeature?.Error is NoDataException)
            {
                await ExceptionHandlerHelper.HandleException(httpContext, exceptionHandlerFeature, 417);
                // return;        
            }
            if (exceptionHandlerFeature?.Error is ArgumentNullException)
            {
                await ExceptionHandlerHelper.HandleException(httpContext, exceptionHandlerFeature, 418);
                // return;       
            }
            if (exceptionHandlerFeature?.Error is IncorrectDataException)
            {
                await ExceptionHandlerHelper.HandleException(httpContext, exceptionHandlerFeature, 422);
                // return;        
            }
            if (exceptionHandlerFeature?.Error != null)
            {
                await ExceptionHandlerHelper.HandleException(httpContext, exceptionHandlerFeature, 400);
                // return;       
            }
        });
        return applicationBuilder;
    }
}
