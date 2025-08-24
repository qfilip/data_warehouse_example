using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Entities;
using DwHouse.DataAccess.Records;

namespace DwHouse.UnitTests.Utilities;

public static class DataFaking
{
    public static DateTime Now = new DateTime(2000, 1, 1);

    public static Dictionary<Type, Delegate> FakingFunctions = new()
    {
        {
            typeof(ExampleEntity),
            () => MakeEntity(() => new ExampleEntity { Text = "test" })
        },
    };

    private static T MakeEntity<T>(Func<T> retn)
        where T : IId<Guid>, IAuditedEntity<Audit>, ISoftDeletable
    {
        var e = retn();

        e.Id = e.Id != Guid.Empty ? e.Id : Guid.NewGuid();

        e.AuditRecord = new()
        {
            CreatedAt = Now,
            ModifiedAt = Now
        };

        return e;
    }
}
