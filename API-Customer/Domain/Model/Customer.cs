using Domain.Exception;
using System.Text.RegularExpressions;

namespace Domain.Model;

public sealed class Customer 
{
    public Guid? Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Cpf { get; private set; }
    public DateTime BithDate { get; private set; }
    public DateTime RegisterDate { get; private set; }

    public Customer(Guid? id, string name, string email, string cpf, DateTime bithDate)
    {
        var registerDate = DateTime.Now;
        ValidateDomain(name, email, cpf, bithDate, registerDate);

        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
        BithDate = bithDate;
        RegisterDate = registerDate;
    }

    public Customer(Guid? id, string name, string email, string cpf, DateTime bithDate, DateTime registerDate)
    {
        ValidateDomain(name, email, cpf, bithDate, registerDate);

        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
        BithDate = bithDate;
        RegisterDate = registerDate;
    }

    private void ValidateDomain(string name, string email, string cpf, DateTime bithDate, DateTime registerDate)
    {
        Regex regex;

        // Name
        DomainExecptionValidation.When(string.IsNullOrEmpty(name),
               "Nome não informado");

        string trimName = name.Trim();
        DomainExecptionValidation.When(trimName.Length > 100,
               "Nome maior que 100 caracteres");

        // Email
        regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                DomainExecptionValidation.When(string.IsNullOrEmpty(email),
               "E-mail não informado");

        string trimEmail = email.Trim();
        DomainExecptionValidation.When(trimEmail.Length > 100,
               "E-mail maior que 100 caracteres");

        DomainExecptionValidation.When(!regex.IsMatch(trimEmail),
               "E-mail inválido");

        // CPF
        DomainExecptionValidation.When(string.IsNullOrEmpty(cpf),
               "CPF não informado");

        string trimCpf = regex.Replace(cpf.Trim(), "");
        DomainExecptionValidation.When(trimCpf.Length != 11,
               "CPF inválido");

        // Bith Date
        DomainExecptionValidation.When(bithDate == DateTime.MinValue,
               "Data de Nascimento inválida");

        // Register Date
        DomainExecptionValidation.When(registerDate == DateTime.MinValue,
               "Data de Registro inválida");
    }
}
