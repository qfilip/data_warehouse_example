using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DwHouse.DataAccess.Entities.Store;

public class Customer : IId<Guid>
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}
