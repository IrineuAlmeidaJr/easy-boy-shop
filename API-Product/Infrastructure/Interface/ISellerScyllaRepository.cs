using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ISellerScyllaRepository
{
    Task<SellerEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<SellerEntity>?> GetSellers();
}
