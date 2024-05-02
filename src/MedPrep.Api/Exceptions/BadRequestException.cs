namespace MedPrep.Api.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message) { }

    public BadRequestException(string message, Exception inner)
        : base(message, inner) { }
}
