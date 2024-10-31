using Domain.Model;

namespace Domain.Interface;

public interface IStockRepository
{
    Task<Stock> SaveAsync(Stock stock);
    Task<IEnumerable<Stock>?> GetStocksAsync();
    Task<Stock?> GetStockByIdAsync(Guid id);
    Task<bool> DeleteStockAsync(Guid id);
}
