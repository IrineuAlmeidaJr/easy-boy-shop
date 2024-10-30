namespace Application.DTO;

public class ProductRequest
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SellerId { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
