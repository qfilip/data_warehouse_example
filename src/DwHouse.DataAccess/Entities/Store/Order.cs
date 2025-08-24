using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DwHouse.DataAccess.Entities.Store;

public class Order : IId<Guid>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime PlacedOn { get; set; }
    public DateTime? CompletedOn { get; set; }

    public Customer? Customer { get; set; }
    public Product? Product { get; set; }
}