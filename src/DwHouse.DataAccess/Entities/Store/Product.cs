using DwHouse.DataAccess.Abstractions;

namespace DwHouse.DataAccess.Entities.Store;

public class Product : IId<Guid>
{
    public Product()
    {
        Orders = new HashSet<Order>();
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}