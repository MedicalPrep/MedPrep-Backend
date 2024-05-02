namespace MedPrep.Api.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message) { }

    public ConflictException(string message, Exception inner)
        : base(message, inner) { }
}
