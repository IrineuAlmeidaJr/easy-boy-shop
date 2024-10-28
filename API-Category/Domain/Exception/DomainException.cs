using System.Net;

namespace Domain.Exception;

[Serializable]
public abstract class DomainException : SystemException
{
    public string Title { get; }
    public string Description { get; }
    public HttpStatusCode StatusCode { get; }

    protected DomainException(string title, string description, HttpStatusCode statusCode)
        : base($"{title}: {description}")
    {
        Title = title;
        Description = description;
        StatusCode = statusCode;
    }

    protected DomainException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}
