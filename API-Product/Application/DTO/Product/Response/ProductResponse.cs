using Application.DTO.Category;
using Application.DTO.Seller;
using Application.DTO.Stock;

namespace Application.DTO;

public class ProductResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public CategoryDto? Category { get; set; }
    public SellerDto? Seller { get; set; }
    public StockDto? Stock { get; set; }
    public decimal Price { get; set; }
}
