using DwHouse.DataAccess.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Dbe = DwHouse.DataAccess.Contexts.DbContextExtensions;

namespace DwHouse.DataAccess.Contexts;

public class StoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionStrings.Store);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Dbe.ConfigureEntity(modelBuilder.Entity<Customer>(), e =>
        {
            e.ToTable("Customers");
            e.Property(x => x.Name).IsRequired();
        });

        Dbe.ConfigureEntity(modelBuilder.Entity<Order>(), e =>
        {
            e.ToTable("Orders");
            e.Property(x => x.Amount).IsRequired();

            e.HasOne(x => x.Customer).WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName($"FK_{nameof(Customer)}_{nameof(Order)}");

            e.HasOne(x => x.Product).WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName($"FK_{nameof(Product)}_{nameof(Order)}");
        });

        Dbe.ConfigureEntity(modelBuilder.Entity<Product>(), e =>
        {
            e.ToTable("Products");
            e.Property(x => x.Name).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Dbe.AuditEntries(this);
        return base.SaveChangesAsync(cancellationToken);
    }
}
