using Domain.Model;

namespace Domain.Interface;

public interface ISellerRepository
{
    Task<Seller> SaveAsync(Seller seller);
    Task<IEnumerable<Seller>?> GetSellersAsync();
    Task<Seller?> GetSellerByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
}
