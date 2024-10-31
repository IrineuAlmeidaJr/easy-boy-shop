using Domain.Event;
using Domain.Model;

namespace Application.Interface;

public interface IStockMapper
{
    public Stock FromProductCreatedEvent(ProductCreatedEvent stockEvent);
}
