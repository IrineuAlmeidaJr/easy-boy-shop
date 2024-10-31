namespace Domain.Model;

public sealed class Stock
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public Stock(Guid? id, Guid productId, int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }
}
