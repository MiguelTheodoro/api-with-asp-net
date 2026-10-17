
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class Prevent: IExceptionHandler
{
    private ILogger<Prevent> logger;

    public Prevent(ILogger<Prevent> logger)
    {
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if(exception is ValidationException validation)
        {
            
            logger.LogWarning(validation, "The application encountered an error while applying the evaluation to the request");

            ProblemDetails result = new ProblemDetails { Status = StatusCodes.Status400BadRequest, Type = "https://ietf.org", Title = "Error in data validation", Detail = validation.Message };

           // httpContext.Response.StatusCode = result.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;

        }

        if(exception is ValidationException serialization)
        {
            logger.LogWarning(serialization, "The application encountered an error while deserializing the body request");

            ProblemDetails result = new ProblemDetails { Status = StatusCodes.Status400BadRequest, Type = "https://ietf.org", Title = "Error in deserialization error", Detail = "The data is bad formated or " };

            //httpContext.Response.StatusCode = result.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }

        return false;


    }
}