using Application.DTO;
using Domain.Model;

namespace Application.Interface;

public interface IProductMapper
{
    Product FromProductRequest(ProductRequest productRequest);
    ProductDto ToProductDto(Product product);
    ProductResponse ToProductResponse(Product customer);
}
