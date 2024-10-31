using Application.Interface;
using Domain.Event;
using Domain.Model;

namespace Application.Mapper;

public class StockMapper : IStockMapper
{
    public Stock FromProductCreatedEvent(ProductCreatedEvent stockEvent) => new(
        null,
        stockEvent.ProductId,
        stockEvent.Quantity
    );
}
