using System.Net;

namespace Domain.Exception;

public class DomainExecptionValidation : DomainException
{
    private const string DEFAULT_TITLE = "Erro de Validação";

    public DomainExecptionValidation(string title, string description) 
        : base(title, description, HttpStatusCode.UnprocessableEntity)
    { }

    public static void When(bool hasError, string description)
    {
        if (hasError)
            throw new DomainExecptionValidation(DEFAULT_TITLE, description);
    }
}