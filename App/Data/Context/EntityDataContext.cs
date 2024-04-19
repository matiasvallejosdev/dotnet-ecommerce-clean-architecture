namespace App.Data;

using Microsoft.EntityFrameworkCore;

public class EntityDataContext : DbContext
{
    public EntityDataContext(DbContextOptions<EntityDataContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Family> Families { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define a unique index on the 'Code' column of the 'Products' table
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Code)
            .IsUnique();
    }
}