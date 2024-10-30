using Domain.Model;

namespace Infrastructure.Repository.Entity;

public class ProductEntity
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SellerId { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
