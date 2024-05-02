namespace MedPrep.Api.ExceptionHandlers;

using MedPrep.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<BadRequestExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not BadRequestException badRequestException)
        {
            return false;
        }

        this.logger.LogError(
            badRequestException,
            "Exception occurred: {Message}",
            badRequestException.Message
        );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = badRequestException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
