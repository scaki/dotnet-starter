using System.Net;

namespace CatalogService.Application.Exceptions;

public class SCException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public override string? Message { get; }
    public Exception Exception { get; set; }

    public SCException(string? message, Exception exception) : base(message, exception)
    {
        Message = message;
        Exception = exception;
    }

    public SCException(HttpStatusCode statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public SCException(HttpStatusCode statusCode, string? message, Exception exception) : base(message, exception)
    {
        StatusCode = statusCode;
        Message = message;
        Exception = exception;
    }
}