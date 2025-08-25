using Microsoft.EntityFrameworkCore;

namespace DwHouse.DataAccess.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options) : base(options)
	{
    }

    
}