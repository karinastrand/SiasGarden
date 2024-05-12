using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
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
                Name = "Ettåriga",
                DisplayOrder = 3
            },
            new Category 
            {
                Id=4,
                Name="Rhododendron",
                DisplayOrder = 4
            },
            
            new Category
            {
                Id = 5,
                Name = "Hortensia",
                DisplayOrder = 5
            },
            new Category
            {
                Id = 6,
                Name = "Buskar",
                DisplayOrder = 6
            },
            new Category
            {
                Id = 7,
                Name = "Träd",
                DisplayOrder = 7
            },
            new Category
            {
                Id = 8,
                Name = "Ormbunkar",
                DisplayOrder = 8
            }
           );
        modelBuilder.Entity<SubCategory>().HasData(
            new SubCategory
            {
                Id = 1,
                Name = "Kryddväxt"
            },
            new SubCategory
            {
                Id = 2,
                Name = "Buskros"
            },
           new SubCategory
           {
               Id = 3,
               Name = "Rabattros"
           },
            new SubCategory
            {
                Id = 4,
                Name = "Azalea"
            },

            new SubCategory
            {
                Id = 5,
                Name = "Parkrhododendron"
            },
            new SubCategory
            {
                Id = 6,
                Name = "Jordröksväxt"
            },
           
            new SubCategory
            {
                Id = 7,
                Name = "Bärbuskar"
            },
            new SubCategory
            {
                Id = 8,
                Name = "Fruktträd"
            },
             new SubCategory
             {
                 Id = 9,
                 Name = "Vidjehortensia"
             },
              new SubCategory
              {
                  Id = 10,
                  Name = "Vipphortensia"
              },
               new SubCategory
               {
                   Id = 11,
                   Name = "Klätterhortensia"
               }
           );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Nostalgi",
                LatinName ="Rosa Taneiglat",
                Description ="Sensationell storblommig ratbttros med stora, fyllda, svagt doftande gräddvita" +
                " blommor med körsbärsröda kanter. Blommar från juni till oktober. Trivs i sol-halvskugga i väldränerad, " +
                "näringsrik jord",
                Price =699,
                CategoryId=1,
                SubCategoryId=3
            },
            new Product
            { 
                Id = 2,
                Name = "Lampion",
                LatinName ="Rose Lampion",
                Description="Otroligt vacker ros med buskigt växtsätt. Remonterar från sommar till höst med fylllda gula" +
                " blommor och röda anstrykningar på yttre kronbladen. Friskt sort. Trovs soligt i väldränerad, näringsrik jord.",
                Price=999,           
                CategoryId=1,
                SubCategoryId = 3
            },
            new Product
            {
                Id = 3,
                
                Name = "Löjtnatshjärta Valentine",
                LatinName = "Lamprocapnos specabilis Valentine",
                Description ="Vacker lättodlad perenn med hjärformade blommor i rött och vitt. Klipp ner efter blomning " +
                "för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                Price=199,              
                CategoryId=2,
                SubCategoryId = 6
            },
            new Product
            {
                Id = 4,
                Name = "Löjtnadshjärta",
                LatinName = "Lamprocapnos specabilis",
                Description = "Vacker lättodlad perenn med hjärformade blommor i rosa och vitt. Klipp ner efter blomning " +
                "för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                Price = 179,
              
                CategoryId = 2,
                SubCategoryId = 6
            },

            new Product
            {
                Id = 5,
                Name = "Hansestadt Rostock",
                Description="Klasblommig ros med kompakt växtsätt. Remonterar från sommar till höst med fyllda aprikosgula " +
                "blommor. Medelstark doft. Frisk sort. Trivs i sol-halvskugga i väldränerad näringsrik jord.",
                Price=699,
              
                CategoryId=1,
                SubCategoryId = 3
            },
            new Product
            {
                Id = 6,
                Name = "Purpurtimjan",
                LatinName ="Thymus Coccineus",
                Description="Mattbildande låg timjan som med sitt kompakta växtsätt av små gröna blad och rik " +
                "blomning i rödviolett blir perfekt marktäckare i stenparti eller som kantväxt. Trivs soligt i väldränerad, "+
                "mager jord.",
                Price=59.9, 
                
                CategoryId=2,
                SubCategoryId = 1
            },
            new Product
            {
                Id = 7,
                Name = "Sundaw fraise",
                LatinName="Hudrangea panicilata Sundae fraise",
                Description="Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de" +
                " åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.",
                Price=699,
                
                CategoryId=5,
                SubCategoryId=10

            },
            new Product
            {
                Id = 8,
                Name = "Living Pink & Rose",
                LatinName = "Hudrangea panicilata Living Pink & Rose",
                Description = "Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de" +
                " åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.",
                Price = 349,
                
                CategoryId = 5,
                SubCategoryId = 10
            }
           );

    }

}
