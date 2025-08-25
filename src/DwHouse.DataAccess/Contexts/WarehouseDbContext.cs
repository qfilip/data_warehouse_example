using DwHouse.DataAccess.Entities.Warehouse;
using Microsoft.EntityFrameworkCore;
using Dbe = DwHouse.DataAccess.Contexts.DbContextExtensions;

namespace DwHouse.DataAccess.Contexts;

public class WarehouseDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionStrings.Warehouse);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
    public DbSet<Fact> Facts { get; set; }
    public DbSet<OrderDimension> Orders { get; set; }
    public DbSet<CustomerDimension> Customers { get; set; }
    public DbSet<ProductDimension> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Dbe.ConfigureEntity(modelBuilder.Entity<Fact>(), e =>
        {
            e.ToTable("Facts");
            e.HasKey(x => x.Id);

            e.OwnsOne(x => x.AuditRecord);

            e.HasOne(x => x.Product).WithMany(x => x.Facts)
                .HasForeignKey(x => x.ProductId);

            e.HasOne(x => x.Order).WithMany(x => x.Facts)
                .HasForeignKey(x => x.OrderId);

            e.HasOne(x => x.Customer).WithMany(x => x.Facts)
                .HasForeignKey(x => x.CustomerId);
        });

        Dbe.ConfigureEntity(modelBuilder.Entity<OrderDimension>(), e => e.ToTable("Orders"));
        Dbe.ConfigureEntity(modelBuilder.Entity<CustomerDimension>(), e => e.ToTable("Customers"));
        Dbe.ConfigureEntity(modelBuilder.Entity<ProductDimension>(), e => e.ToTable("Products"));

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Dbe.AuditEntries(this);
        return base.SaveChangesAsync(cancellationToken);
    }
}
