using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace InstaAPI.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Title = "Errores de validación";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Extensions.Add("errors", validationException.Errors.Select(e => new
            {
                property = e.PropertyName,
                message = e.ErrorMessage
            }));
        }
        else
        {
            problemDetails.Title = "Error interno del servidor";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = exception.Message;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
