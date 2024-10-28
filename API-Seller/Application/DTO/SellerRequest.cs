namespace Application.DTO;
public class SellerRequest
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Cnpj { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Logradouro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
}
