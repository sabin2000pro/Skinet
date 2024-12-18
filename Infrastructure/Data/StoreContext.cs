using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class StoreContext : DbContext

{
    public StoreContext(DbContextOptions options) : base(options) {}

    public DbSet<Product> Product {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
        modelBuilder.Entity<Product>().Property(x => x.Price);
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

}