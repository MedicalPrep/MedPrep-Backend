namespace MedPrep.Api.ExceptionHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;
using MedPrep.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<UnauthorizedExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not UnauthorizedException unAuthorizedRequestException)
        {
            return false;
        }

        this.logger.LogError(
            unAuthorizedRequestException,
            "Exception occurred: {Message}",
            unAuthorizedRequestException.Message
        );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Bad Request",
            Detail = unAuthorizedRequestException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
