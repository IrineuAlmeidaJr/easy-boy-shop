using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface IProductScyllaRepository
{
    Task<ProductEntity> SaveAsync(ProductEntity entity);
    Task<ProductEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductEntity>?> GetProducts();
    Task<bool> DeleteAsync(Guid id);
}
