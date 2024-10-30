using Application.DTO.Category;
using Application.DTO.Seller;
using Application.DTO.Stock;
using Domain.Model;

namespace Application.DTO;

public class ProductDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
