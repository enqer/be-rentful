using System.Net;

namespace Rentful.Domain.Exceptions
{
    public class ExceptionMessage
    {
        public ExceptionMessage(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; }
        public string Description { get; }
    }

    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode statusCode, string title, string description, string? internalMessage = null) : this(statusCode, new ExceptionMessage(title, description), internalMessage)
        {
        }
        public HttpResponseException(HttpStatusCode statusCode, ExceptionMessage message, string? internalMessage = null)
        {
            StatusCode = statusCode;
            Value = message;
            InternalMessage = internalMessage;
        }

        public HttpStatusCode StatusCode { get; }
        public ExceptionMessage Value { get; }
        public string? InternalMessage { get; }
    }
}
