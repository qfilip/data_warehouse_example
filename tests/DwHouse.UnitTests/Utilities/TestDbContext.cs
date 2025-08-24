using Microsoft.EntityFrameworkCore;
using DwHouse.DataAccess.Contexts;
using DwHouse.Identity.Abstractions;
using DwHouse.Identity.Models;

namespace DwHouse.UnitTests.Utilities;

public class TestDbContext : AppDbContext
{
    public TestDbContext(
        DbContextOptions<AppDbContext> options,
        IAppIdentityService<AppIdentity> _appIdentityService) : base(options)
    {
    }

    public void Clear() => ChangeTracker.Clear();

    public override int SaveChanges()
    {
        var result = base.SaveChanges();
        base.ChangeTracker.Clear();

        return result;
    }
}
