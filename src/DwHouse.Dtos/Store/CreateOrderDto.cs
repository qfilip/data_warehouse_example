namespace DwHouse.Dtos.Store;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
}
