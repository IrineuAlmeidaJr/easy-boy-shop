using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ISellerScyllaRepository
{
    Task<SellerEntity> SaveAsync(SellerEntity entity);
    Task<SellerEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<SellerEntity>?> GetSellers();
    Task DeleteAsync(Guid id);
}
