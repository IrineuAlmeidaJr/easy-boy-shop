using Domain.Event;

namespace Application.Interface;

public interface IProductCreatedEventPublisher
{
    Task PublishAsync(ProductCreatedEvent payload);
}
