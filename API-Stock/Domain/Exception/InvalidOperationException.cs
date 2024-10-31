using System.Net;

namespace Domain.Exception;

public class InvalidOperationException : DomainException
{
    private const string DEFAULT_TITLE = "Operação Inválida: ";

    public InvalidOperationException(string description) :
        base(DEFAULT_TITLE, description, HttpStatusCode.Conflict)
    { }
}
