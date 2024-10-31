using Domain.Model;

namespace Infrastructure.Interface;

public interface IStockScyllaRepository
{
    Task<Stock> SaveAsync(Stock entity);
    Task<Stock?> GetByIdAsync(Guid id);
    Task<IEnumerable<Stock>?> GetStocksAsync();
}
