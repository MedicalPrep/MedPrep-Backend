namespace MedPrep.Api.ExceptionHandlers;

using MedPrep.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<NotFoundExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not NotFoundException notFoundRequestException)
        {
            return false;
        }

        this.logger.LogError(
            notFoundRequestException,
            "Exception occurred: {Message}",
            notFoundRequestException.Message
        );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not Found",
            Detail = notFoundRequestException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
