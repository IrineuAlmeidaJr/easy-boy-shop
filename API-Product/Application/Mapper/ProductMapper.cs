using Application.DTO;
using Application.DTO.Category;
using Application.DTO.Seller;
using Application.DTO.Stock;
using Application.Interface;
using Domain.Model;

namespace Application.Mapper;

public class ProductMapper : IProductMapper
{
    public Product FromProductRequest(ProductRequest productRequest) => new(
        productRequest.Id,
        productRequest.Name,
        new Category(productRequest.CategoryId),
        new Seller(productRequest.SellerId),
        null,
        productRequest.Price
    );

    public ProductDto ToProductDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };

    public ProductResponse ToProductResponse(Product product) => new ()
    {
        Id = product.Id,
        Name = product.Name,
        Category = product.Category == null ? null :new CategoryDto
        {
            Id = product.Category.Id,
            Name = product.Name
        },
        Seller = product.Seller == null ? null :new SellerDto
        {
            Id = product.Seller.Id,
            Name = product.Seller.Name
        },
        Stock = product.Stock == null ? null : new StockDto
        {
            Id = product.Stock.Id,
            Quantity = product.Stock.Quantity
        },
        Price = product.Price
    };
      
}
