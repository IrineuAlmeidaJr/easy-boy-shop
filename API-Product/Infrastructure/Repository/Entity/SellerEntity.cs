namespace Infrastructure.Repository.Entity;

public class SellerEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Logradouro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}