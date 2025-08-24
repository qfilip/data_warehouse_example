using DwHouse.DataAccess.Abstractions;

namespace DwHouse.DataAccess.Entities.Warehouse;

public class CustomerDimension : IId<Guid>
{
    public CustomerDimension()
    {
        Facts = new HashSet<Fact>();    
    }
    public ICollection<Fact> Facts { get; set; }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}
