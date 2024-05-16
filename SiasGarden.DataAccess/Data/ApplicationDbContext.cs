using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiasGarden.Models;
using System.Reflection;

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
                Name = "Rosor"
              
            },
            new Category
            {
                Id = 2,
                Name = "Perenner"
            },
            new Category
            {
                Id = 3,
                Name = "Klängväxter"
             
            },
            new Category 
            {
                Id=4,
                Name="Buskar"
             
            },
            
            new Category
            {
                Id = 5,
                Name = "Träd"
                
            }
           );
        modelBuilder.Entity<SubCategory>().HasData(
            new SubCategory
            {
                Id = 1,
                Name = "Kryddväxt",
                CategoryId=2
            },
            new SubCategory
            {
                Id = 2,
                Name = "Löjtnadshjärta",
                CategoryId = 2
            },
             new SubCategory
             {
                 Id = 3,
                 Name = "Buskrosor",
                 CategoryId =1
             },
            new SubCategory
            {
                Id = 4,
                Name = "Rabattrosor",
                CategoryId = 1
            },
            new SubCategory
            {
                Id = 5,
                Name = "Klematis",
                CategoryId = 3
            },
            new SubCategory
            {
                Id = 6,
                Name = "Kaprifol",
                CategoryId = 3
            },
            new SubCategory
            {
                Id = 7,
                Name = "Prydnadsbuskar",
                CategoryId = 4
            },
            new SubCategory
            {
                Id = 8,
                Name = "Bärbuskar",
                CategoryId = 4
            },

            new SubCategory
            {
                Id = 9,
                Name = "Prydnadsträd",
                CategoryId = 5
            },
            new SubCategory
            {
                Id = 10,
                Name = "Fruktträd",
                CategoryId = 5
            },
            new SubCategory
            {
                Id = 11,
                Name = "Äpple",
                CategoryId = 5
            },
            new SubCategory
            {
                Id = 12,
                Name = "Plommon",
                CategoryId = 5
            },
            new SubCategory
            {
                Id = 13,
                Name = "Svarta vinbär",
                CategoryId = 4
            },
            new SubCategory
            {
                Id = 14,
                Name = "Kivi",
                CategoryId = 4
            },
            new SubCategory
            {
                Id = 15,
                Name = "Hortensia",
                CategoryId = 4
            }



           );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Lavendel 'Hidcote'",
                LatinName ="Lavendula augustifolia 'Hidcote'",
                Description = "Blommar rikligt med mörkt blåviolett blommor och har ett kompakt, lågväxande växtsätt. Utvecklas bäst i full sol på väldränerad jord. En av de härdigaste sorterna.",
                Light="Sol",
                Height=60,
                Price =59,
                ZoneTo =5,
                SubCategoryId=1
            },
            new Product
            { 
                Id = 2,
                Name = "Stäppsalvia",
                LatinName ="Salvia nemorosa 'Caradonna'",
                Description= "Blomvillig och lättskött perenn för soliga lägen med djupt, mörklila blomax på purpurfärgade stjälkar. Blommar rikligt och länge.",
                Price=49,
                Light = "Sol",
                Height = 60,
               
                ZoneTo = 3,
                SubCategoryId = 1

            },
            new Product
            {
                Id = 3,
                
                Name = "Valentine",
                LatinName = "Lamprocapnos specabilis 'Valentine'",
                Description ="Vacker lättodlad perenn med hjärformade blommor i rött och vitt. Klipp ner efter blomning " +
                "för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                Price=199,
                Light = "Sol till halvskugga",
                Height = 40,
                
                ZoneTo = 3,
                SubCategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Löjtnadshjärta 'Alba'",
                LatinName = "Lamprocapnos spectabilis 'Alba'",
                Description = "Vacker, lättodlad perenn med hjärtformade blommor i vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge. " +
                "för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                Price = 84,
                Light = "Sol till halvskugga",
                Height = 40,
                
                ZoneTo = 9,
                SubCategoryId = 2
            },

            new Product
            {
                Id = 5,
                Name = "Hansestadt Rostock",
               
                Description ="Klasblommig ros med kompakt växtsätt. Remonterar från sommar till höst med fyllda aprikosgula " +
                "blommor. Medelstark doft. Frisk sort. Trivs i sol-halvskugga i väldränerad näringsrik jord.",
                Price = 289,
                Light = "Sol till halvskugga",
                Height = 80,
                
                ZoneTo = 4,
                SubCategoryId = 4
            },
            new Product
            {
                Id = 6,
                Name = "Nostalgi",
                
                Description= "Sensationell storblommig rabattros med stora, fyllda, svagt doftande gräddvita blommor med körsbärsröda kanter. Blommar från juni till oktober. Trivs i sol-halvskugga i väldränerad, näringsrik jord.",
                Price = 299,
                Light = "Sol",
                Height = 70,
                
                ZoneTo = 4,
                SubCategoryId = 4


            },
            new Product
            {
                Id = 7,
                Name = "Isabel Renaissance",
               
                Description = "Buskros med upprätt och tätt växtsätt. Bladverket är läderartat, mörkgrönt och glänsande. Blommar med tätt fyllda, mörkt sammetsröda blommor som har en medelstark doft. Riklig blomning nästan oavbrutet från juni och in på senhösten. Mycket användbar då den kan planteras i grupper, tillsammans med perenner, som kantväxt och häckväxt," +
                " blir även fin i kruka. Trivs bäst i soliga lägen i näringsrik jord.",
                Price = 345,
                Light = "Sol",
                Height = 150,
               
                ZoneTo = 3,
                SubCategoryId = 3

            },
            new Product
            {
                Id = 8,
                Name = "High Voltage",
               
                Description = "Buskros med ett kraftigt, buskigt växtsätt. Bladverket är friskt, grönt där all nytillväxt är vackert röd. Praktfull blomning med klasar av gula, fyllda blommor som ljusnar under blomningen till en härlig svavelgul nyans. " +
                "Doftar angenämt och tål regn bra. Är en frisk ros som är motståndskraftig mot olika svampsjukdomar",
                Price = 345,
                Light = "Sol",
                Height = 100,
                
                ZoneTo = 5,
                SubCategoryId = 3
            },
             new Product
             {
                 Id = 9,
                 Name = "Clematis Hisako",
                
                 Description = "Vacker hybrid som blommar länge från sommar till tidig höst med sammetsröda blommor mot mörkgrönt bladverk. Trivs i sol–halvskugga i fukthållande, väldränerad jord. Beskärningsgrupp 2.",
                 Light = "Sol-halvskugga",
                 Height = 200,
                 Price = 350,
                
                 ZoneTo = 6,
                 SubCategoryId = 5
             },
              new Product
              {
                  Id = 10,
                  Name = "Clematis 'Ville de Lyon'",
                 
                  Description = "Storblommig klematis med läckert körsbärsröda blommor. Blommar under en lång tid. Blomrik och tålig. För soliga till halvskuggiga lägen. Beskärningsgrupp 3.",
                  Light = "Sol-halvskugga",
                  Height = 400,
                  Price = 199,
                 
                  ZoneTo = 5,
                  SubCategoryId = 5
              },
              new Product
              {
                  Id = 11,
                  Name = "Kaprifol 'Goldflame'",
                  LatinName = "Lonicera heckrottii 'Goldflame'",
                  Description = "Lonicera heckrottii 'Goldflame' är en lättodlad och vacker kaprifol. Sorten är kraftfull och snabbväxande. Blommar med rörformiga, rosa/rödrosa blommor under juni - augusti. På hösten kommer dekorativa oätliga röda bär. Bladen är ovala, medelstora, blågröna och ger fin gul höstfärg. Doften är frisk, intensivt och fantastisk. " +
                  "Sorten är mycket tålig och ger riklig blomning på en solig plats. Älskas av fjärilar, bin och humlor",
                  Light = "Sol-halvskugga",
                  Height = 350,
                  Price = 28,
                  
                  ZoneTo = 3,
                  SubCategoryId = 6
              },
              new Product
              {
                  Id = 12,
                  Name = "Tellmannskaprifol",
                  LatinName = "Lonicera x tellmanniana'",
                  Description = "Färggrann sort med kraftigt, slingrande och täckande växtsätt med frodigt blågrönt bladverk. Blommar rikligt med doftande," +
                  " brandgula blommor varvat med bronsgula knoppar. Bildar röda, oätliga bär. Trivs i sol till halvskugga på väldränerad, näringsrik jord.3.",
                  Light = "Sol-halvskugga",
                  Height = 450,
                  Price = 199,
                 
                  ZoneTo = 4,
                  SubCategoryId =6
              },
              new Product
              {
                  Id = 13,
                  Name = "Vanille-Fraise",
                  LatinName = "Hydrangea paniculata 'Vanille-Fraise'",
                  Description = "Elegant, storblommig sort med buskigt, upprätt växtsätt. Blommar länge med gräddvita, fylliga klasar som skiftar till mörkrosa och avslutas i mörkrosa nyanser. Trivs i sol till halvskugga på mullrik, lätt kemiskt sur jord.",
                  Light = "Sol-halvskugga",
                  Height = 250,
                  Price = 299,
                  
                  ZoneTo = 5,
                  SubCategoryId = 15
              },
              new Product
              {
                  Id = 14,
                  Name = "Syrenbuddleja",
                  LatinName = "Buddleja davidii 'Royal Red'",
                  Description = "Syrenbuddleja kallas även för fjärilsbuske, då den är känd för att locka till sig fjärilar. Fungerar att planteras både som solitär " +
                  "och i grupp. Anses vara mindre smaklig för rådjur.",
                  Height = 199,
                  Price = 340,
                 
                  ZoneTo = 3,
                  SubCategoryId =7
              },
              new Product
              {
                  Id = 15,
                  Name = "Svarta vinbär 'Öjebyn'",
                  LatinName = "Ribes nigrum 'Öjebyn'",
                  Description = "Kraftig med lätt överhängande grenar och rik skörd av svarta bär i rejäla klasar. Härdig och motståndskraftig mot mjöldagg." +
                  " Trivs i sol-halvskugga i väldränerad, näringsrik, fuktighetshållande jord.",
                  Light = "Sol-halvskugga",
                  Height = 150,
                  Price = 149,
                  
                  ZoneTo = 6,
                  SubCategoryId =13
              },
              new Product
              {
                  Id = 16,
                  Name = "Kivi 'Jenny'",
                  LatinName = "Actinidia deliciosa 'Jenny'",
                  Description = "Frodig slingerväxt som får gräddvita, doftande blommor och små äggformade, bruna frukter med hårigt skal och grönt," +
                  " sötsyrligt fruktkött. Självfertil men gynnas av samplantering med hanplanta. Ger bäst skörd på en varm solig och skyddad plats, gärna i växthus.",
                  Light = "Sol",
                  Height = 400,
                  Price = 379,
                  
                  ZoneTo = 1,
                  SubCategoryId =14
              },
              new Product
              {
                  Id = 17,
                  Name = "Aroma",
                  LatinName = "Malus domestica 'Aroma'",
                  Description = "Medelstarkväxande, populär sort som ger gulgröna äpplen med röd solsida. Har ett fint, fast fruktkött med en sötsyrlig smak." +
                  " Frisk sort. Kan lagras fram till jul. Pollineras av bl. a 'Alice' och 'Ingrid Marie'. Bäst skörd i soligt läge.",
                  Light = "Sol-halvskugga",
                  Height = 500,
                  Price = 520,
                 
                  ZoneTo = 4,
                  SubCategoryId = 11
              },
              new Product
              {
                  Id = 18,
                  Name = "Victoria",
                  LatinName = "Prunus domestica 'Victoria'",
                  Description = "Svagväxande, populär sort som får stora, medeltidiga skördar med stora, " +
                  "rödgula frukter med ljusviolett daggig yta. Fast fruktkött med söt smak. Självfertil. Ger bäst skörd i soligt läge på väldränerad och näringsrik jord.",
                  Light="Sol-halvskugga",
                  Height = 450,
                  Price = 799,
                 
                  ZoneTo = 3,
                  SubCategoryId = 12
              },
               new Product
               {
                   Id = 19,
                   Name = "Eukalyptusvide",
                   LatinName = "Salix integra 'Hakuro-nishiki'",
                   Description = "Odlingsvärt litet träd med tät, rundad krona. Vackert ljusgrönt bladverk med " +
                   "vitbrokiga skott som har rosatonad anstrykning på nytillväxten. Trivs i skyddat, soligt läge på väldränerad, näringsrik jord.",
                   Light = "Sol",
                   Height = 250,
                   Price = 799,
                   
                   ZoneTo = 2,
                   SubCategoryId = 9
               },
              new Product
              {
                  Id = 20,
                  Name = "Hängsälg",
                  LatinName = "Salix caprea 'Kilmarnock'",
                  Description = "Färggrann sort med kraftigt, slingrande och täckande växtsätt med frodigt blågrönt bladverk. Blommar rikligt med doftande," +
                  " brandgula blommor varvat med bronsgula knoppar. Bildar röda, oätliga bär. Trivs i sol till halvskugga på väldränerad, näringsrik jord.3.",
                  Light = "Sol",
                  Height = 450,
                  Price = 100,
                  
                  ZoneTo = 6,
                  SubCategoryId = 9
              },
              new Product
              {
                  Id = 21,
                  Name = "Sundae-Fraise",
                  LatinName = "Hydrangea paniculata 'Sundae-Fraise'",
                  Description = "Med det nätta formatet och det hängande växtsättet passar hängsälgen i kruka väldigt fint men också mitt i en större plantering för att skapa höjd. Fördelen med att den är uppstammad är att man kan plantera olika sommarblommor" +
                  " eller perenner under den. Samma sak när hängsälg står i rabatten, ett litet träd med videkissar blir perfekt med blommor nedanför.",
                  Light="Sol-halvskugga",
                  Height = 120,
                  Price = 450,
                 
                  ZoneTo = 3,
                  SubCategoryId = 15
              }
           );
        modelBuilder.Entity<ProductImage>().HasData(
           new ProductImage
           {
               Id = 1,
               ImageUrl=@"\images\products\product-1\startbild.jpg",
               ProductId=1
          
           },
          new ProductImage
          {
              Id = 2,
              ImageUrl = @"\images\products\product-2\startbild.jpg",
              ProductId = 2

          },
           new ProductImage
           {
               Id = 3,
               ImageUrl = @"\images\products\product-3\startbild.jpg",
               ProductId = 3

           },
           new ProductImage
           {
               Id = 4,
               ImageUrl = @"\images\products\product-4\startbild.jpg",
               ProductId = 4

           },
          new ProductImage
          {
              Id = 5,
              ImageUrl = @"\images\products\product-5\startbild.jpg",
              ProductId = 5

          },
          new ProductImage
          {
              Id = 6,
              ImageUrl = @"\images\products\product-6\startbild.jpg",
              ProductId = 6

          },
          new ProductImage
          {
              Id = 7,
              ImageUrl = @"\images\products\product-7\startbild.jpg",
              ProductId = 7

          },
          new ProductImage
          {
              Id = 8,
              ImageUrl = @"\images\products\product-8\startbild.jpg",
              ProductId = 8

          },
          new ProductImage
          {
              Id = 9,
              ImageUrl = @"\images\products\product-9\startbild.jpg",
              ProductId = 9

          },
          new ProductImage
          {
              Id = 10,
              ImageUrl = @"\images\products\product-10\startbild.jpg",
              ProductId = 10

          },
          new ProductImage
          {
              Id = 11,
              ImageUrl = @"\images\products\product-11\startbild.jpg",
              ProductId = 11

          },
          new ProductImage
          {
              Id = 12,
              ImageUrl = @"\images\products\product-12\startbild.jpg",
              ProductId = 12

          },
          new ProductImage
          {
              Id = 13,
              ImageUrl = @"\images\products\product-13\startbild.jpg",
              ProductId = 13

          },
          new ProductImage
          {
              Id = 14,
              ImageUrl = @"\images\products\product-14\startbild.jpg",
              ProductId = 14

          },
          new ProductImage
          {
              Id = 15,
              ImageUrl = @"\images\products\product-15\startbild.jpg",
              ProductId = 15

          },
          new ProductImage
          {
              Id = 16,
              ImageUrl = @"\images\products\product-16\startbild.jpg",
              ProductId = 16

          },
          new ProductImage
          {
              Id = 17,
              ImageUrl = @"\images\products\product-17\startbild.jpg",
              ProductId = 17

          },
          new ProductImage
          {
              Id = 18,
              ImageUrl = @"\images\products\product-18\startbild.jpg",
              ProductId = 18

          },
          new ProductImage
          {
              Id = 19,
              ImageUrl = @"\images\products\product-19\startbild.jpg",
              ProductId = 19

          },
          new ProductImage
          {
              Id = 20,
              ImageUrl = @"\images\products\product-20\startbild.jpg",
              ProductId = 20

          },
          new ProductImage
          {
              Id = 21,
              ImageUrl = @"\images\products\product-21\startbild.jpg",
              ProductId = 21

          }

          );
    }

}
