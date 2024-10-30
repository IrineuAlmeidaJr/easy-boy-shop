using Domain.Model;

namespace Infrastructure.Interface;

public interface IStockScyllaRepository
{
    Task<Stock?> GetStockByProductIdAsync(Guid? id);
    Task<IEnumerable<Stock>?> GetStocksAsync();
}
