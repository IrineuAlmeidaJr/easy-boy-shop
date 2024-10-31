using Application.DTO;
using Domain.Event;
using Domain.Model;

namespace Application.Interface;

public interface IStockServices
{
    Task<Stock> Create(ProductCreatedEvent stockEvent);
    Task<IEnumerable<Stock>> GetProducts();
    Task<Stock> GetProductById(Guid id);
}
