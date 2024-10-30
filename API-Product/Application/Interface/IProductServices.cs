using Application.DTO;

namespace Application.Interface;

public interface IProductServices
{
    Task<ProductDto> Create(ProductRequest request);
    Task<IEnumerable<ProductResponse>> GetProducts();
    Task<ProductResponse> GetProductById(Guid id);
}
