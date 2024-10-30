using Domain.Exception;

namespace Domain.Model;

public sealed class Category
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }

    public Category(Guid id)
    {
        Id = id;
    }

    public Category(Guid id, string name)
    {
        ValidateDomain(name);

        Id = id;
        Name = name;        
    }

    private void ValidateDomain(string name)
    {
        DomainExecptionValidation.When(string.IsNullOrEmpty(name),
               "Nome não informado");

        DomainExecptionValidation.When(name.Trim().Length > 100,
               "Nome maior que 100 caracteres");
    }
}
