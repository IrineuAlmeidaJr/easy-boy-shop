using Domain.Model;

namespace Domain.Interface;

public interface IProductRepository
{
    Task<Product> SaveAsync(Product customer);
    Task<IEnumerable<Product>?> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<bool> DeleteProductAsync(Guid id);
}
