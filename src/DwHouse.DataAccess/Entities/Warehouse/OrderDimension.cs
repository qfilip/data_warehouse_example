using DwHouse.DataAccess.Abstractions;

namespace DwHouse.DataAccess.Entities.Warehouse;

public class OrderDimension : IId<Guid>
{
    public OrderDimension()
    {
        Facts = new HashSet<Fact>();
    }
    public ICollection<Fact> Facts { get; set; }

    public Guid Id { get; set; }

    public int Amount { get; set; }
    public DateTime PlacedOn { get; set; }
    public DateTime? CompletedOn { get; set; }
}
