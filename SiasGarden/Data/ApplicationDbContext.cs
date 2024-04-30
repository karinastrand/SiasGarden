using Microsoft.EntityFrameworkCore;
using SiasGarden.Models;

namespace SiasGarden.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(s => s.SubCategories)
            .WithMany(s => s.Products)
            .UsingEntity(j => j.ToTable("ProductsSubCategories")); base.OnModelCreating(modelBuilder);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }


}
