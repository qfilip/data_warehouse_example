namespace DwHouse.DataAccess.Abstractions;

public interface IAuditedEntity<T>
{
    public T AuditRecord { get; set; }
}
