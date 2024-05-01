using Microsoft.EntityFrameworkCore;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(s => s.SubCategories)
            .WithMany(s => s.Products)
            .UsingEntity(j => j.ToTable("ProductsSubCategories")); base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Rosor",
                DisplayOrder = 1
            },
            new Category
            {
                Id = 2,
                Name = "Perenner",
                DisplayOrder = 2
            },
            new Category
            {
                Id = 3,
                Name = "Fröer",
                DisplayOrder = 3
            },
            new Category
            {
                Id = 4,
                Name = "Fröer",
                DisplayOrder = 4
            },
            new Category
            {
                Id = 5,
                Name = "Buskar",
                DisplayOrder = 5
            },
            new Category
            {
                Id = 6,
                Name = "Träd",
                DisplayOrder = 6
            },
            new Category
            {
                Id = 7,
                Name = "Jord",
                DisplayOrder = 7
            },
            new Category
            {
                Id = 8,
                Name = "Redskap",
                DisplayOrder = 8
            }
           );


    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }


}
