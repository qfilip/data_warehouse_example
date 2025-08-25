using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Records;

namespace DwHouse.DataAccess.Entities.Warehouse;

public class Fact : IId<Guid>, IAuditedEntity<ReadOnlyAudit>
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    
    public int Amount { get; set; }
    public DateTime PlacedOn { get; set; }
    public DateTime? CompletedOn { get; set; }

    public ReadOnlyAudit AuditRecord { get; set; } = new();
    
    public OrderDimension? Order { get; set; }
    public ProductDimension? Product { get; set; }
    public CustomerDimension? Customer { get; set; }
}