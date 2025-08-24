using DwHouse.DataAccess.Enums;

namespace DwHouse.DataAccess.Abstractions;

public interface ISoftDeletable
{
    public eEntityStatus EntityStatus { get; set; }
}
