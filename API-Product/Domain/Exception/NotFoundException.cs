using System.Net;

namespace Domain.Exception;

public class NotFoundException : DomainException
{
    private const string DEFAULT_TITLE = "Recurso não encontrado: ";

    public NotFoundException(string description) : 
        base(DEFAULT_TITLE, description, HttpStatusCode.NotFound) { }
}
