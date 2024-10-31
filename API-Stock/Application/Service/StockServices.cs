using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Exception;
using Domain.Model;
using Domain.Event;

namespace Application.Service;

public class StockServices : IStockServices
{
    private IStockRepository _stockRepository;
    private IStockMapper _stockMapper;

    public StockServices(IStockRepository stockRepository, IStockMapper stockMapper)
    {
        _stockRepository = stockRepository;
        _stockMapper = stockMapper;
    }

    public async Task<Stock> Create(ProductCreatedEvent stockEvent)
    {
        var product = _stockMapper.FromProductCreatedEvent(stockEvent);
        var stored = await _stockRepository.SaveAsync(product);

        if (stored == null)
            throw new Domain.Exception.InvalidOperationException("erro interno ao inserir/alterar o produto");

        return stored;
    }

    Task<Stock> IStockServices.GetProductById(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Stock>> IStockServices.GetProducts()
    {
        throw new NotImplementedException();
    }        
}
