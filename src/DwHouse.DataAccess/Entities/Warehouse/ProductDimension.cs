using DwHouse.DataAccess.Abstractions;

namespace DwHouse.DataAccess.Entities.Warehouse;

public class ProductDimension : IId<Guid>
{
    public ProductDimension()
    {
        Facts = new HashSet<Fact>();
    }
    public ICollection<Fact> Facts { get; set; }


    public Guid Id { get; set; }
    public string? Name { get; set; }
}
