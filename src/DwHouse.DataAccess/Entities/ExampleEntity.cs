using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Enums;
using DwHouse.DataAccess.Records;

namespace DwHouse.DataAccess.Entities;
public class ExampleEntity : IId<Guid>, IAuditedEntity<Audit>, ISoftDeletable
{
    public string? Text { get; set; }

    // entity definition
    public Guid Id { get; set; }
    public Audit AuditRecord { get; set; } = new();
    public eEntityStatus EntityStatus { get; set; }
}