namespace Domain.Event;

public class ProductCreatedEvent
{
    public Guid? ProductId { get; set; }
    public int Quantity { get; set; }
}
