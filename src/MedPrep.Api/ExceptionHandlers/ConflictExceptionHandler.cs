namespace MedPrep.Api.ExceptionHandlers;

using MedPrep.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class ConflictExceptionHandler(ILogger<ConflictExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<ConflictExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ConflictException conflictReqestException)
        {
            return false;
        }

        this.logger.LogError(
            conflictReqestException,
            "Exception occurred: {Message}",
            conflictReqestException.Message
        );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Bad Request",
            Detail = conflictReqestException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
