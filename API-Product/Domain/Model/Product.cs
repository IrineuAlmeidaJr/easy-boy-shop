using Domain.Exception;

namespace Domain.Model;

public sealed class Product
{
    public Guid? Id { get; private set; }
    public string? Name { get; private set; }
    public Category? Category { get; private set; }
    public Seller? Seller { get; private set; }
    public Stock? Stock { get; private set; }
    public decimal Price { get; private set; }

    public Product(Guid? id, string? name, Category? category, Seller? seller, Stock? stock, decimal price)
    {
        ValidateDomain(name, price);

        Id = id;
        Name = name;
        Category = category;
        Seller = seller;
        Stock = stock;
        Price = price;
    }

    private void ValidateDomain(string? name, decimal price)
    {
        // Nome
        DomainExecptionValidation.When(string.IsNullOrEmpty(name),
               "Nome não informado");

        DomainExecptionValidation.When(name.Trim().Length > 100,
               "Nome maior que 100 caracteres");

        // Preço
        DomainExecptionValidation.When(price <= 0,
               "Informe o preço do produto");
    }
}
    