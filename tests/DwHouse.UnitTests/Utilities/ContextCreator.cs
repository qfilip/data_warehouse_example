using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DwHouse.DataAccess.Contexts;
using DwHouse.Identity.Models;
using DwHouse.Identity.Services;

namespace DwHouse.UnitTests.Utilities;

public class ContextCreator: IDisposable
{
    public static AppIdentity SystemIdentity =
        new AppIdentity(Guid.Empty, "system", "a@a.com");

    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AppDbContext> _options;
    public ContextCreator()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        var context = new TestDbContext(_options, new AppIdentityService(false, SystemIdentity));
        context.Database.EnsureCreated();
    }
    
    public TestDbContext GetTestDbContext()
    {
        return new TestDbContext(_options, new AppIdentityService(false, SystemIdentity));
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
