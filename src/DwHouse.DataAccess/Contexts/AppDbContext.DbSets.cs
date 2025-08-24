using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DwHouse.DataAccess.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options) : base(options)
	{
    }

    
}