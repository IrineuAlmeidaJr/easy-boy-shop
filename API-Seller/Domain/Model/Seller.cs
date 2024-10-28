using Domain.Exception;
using System.Text.RegularExpressions;

namespace Domain.Model;

public sealed class Seller
{
    public Guid? Id { get; private set; }
    public string? Name { get; private set; }
    public string? CNPJ { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
    public string? Logradouro { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }

    public Seller(Guid? id, string name, string cnpj, string telefone, string email, string logradouro, string cidade, string estado)
    {

        ValidateDomain(name, cnpj, telefone, email, logradouro, cidade, estado);

        Id = id;
        Name = name;
        CNPJ = cnpj;
        Telefone = telefone;
        Email = email;
        Logradouro = logradouro;
        Cidade = cidade;
        Estado = estado;
    }

    private void ValidateDomain(string name, string cnpj, string telefone, string email, string logradouro, string cidade, string estado)
    {
        Regex regex;

        regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");


        // Name       
        DomainExecptionValidation.When(string.IsNullOrEmpty(name),
               "Nome não informado");

        string trimName = name.Trim();
        DomainExecptionValidation.When(trimName.Length > 100,
               "Nome maior que 100 caracteres");

        // CPNJ                     
        DomainExecptionValidation.When(string.IsNullOrEmpty(cnpj),
               "CPNJ não informado");

        string trimCnpj = regex.Replace(cnpj.Trim(), "");
        DomainExecptionValidation.When(trimCnpj.Length != 14,
               "CPNJ inválido");

        //TELEFONE         
        DomainExecptionValidation.When(string.IsNullOrEmpty(telefone),
               "Telefone não informado");


        // Email        
        DomainExecptionValidation.When(string.IsNullOrEmpty(email),
               "E-mail não informado");
        string trimEmail = email.Trim();

        DomainExecptionValidation.When(trimEmail.Length > 100,
               "E-mail maior que 100 caracteres");

        DomainExecptionValidation.When(!regex.IsMatch(trimEmail),
               "E-mail inválido");

        //LOGRADOURO

        DomainExecptionValidation.When(string.IsNullOrEmpty(logradouro),
             "Logradouro não informado");

        //CIDADE

        DomainExecptionValidation.When(string.IsNullOrEmpty(cidade),
             "Cidade não informada");
        //ESTADO

        DomainExecptionValidation.When(string.IsNullOrEmpty(estado),
             "Estado não informado");

    }
}