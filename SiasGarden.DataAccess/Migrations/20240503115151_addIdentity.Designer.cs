﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiasGarden.DataAccess.Data;

#nullable disable

namespace SiasGarden.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240503115151_addIdentity")]
    partial class addIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProductSubCategory", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoriesId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "SubCategoriesId");

                    b.HasIndex("SubCategoriesId");

                    b.ToTable("ProductsSubCategories", (string)null);
                });

            modelBuilder.Entity("SiasGarden.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Rosor"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Perenner"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Fröer"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Rhododendron"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 5,
                            Name = "Buskar"
                        },
                        new
                        {
                            Id = 6,
                            DisplayOrder = 6,
                            Name = "Träd"
                        },
                        new
                        {
                            Id = 7,
                            DisplayOrder = 7,
                            Name = "Tillbehör"
                        },
                        new
                        {
                            Id = 8,
                            DisplayOrder = 8,
                            Name = "Redskap"
                        });
                });

            modelBuilder.Entity("SiasGarden.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BulkDiscount")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LatinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("StartImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZoneFrom")
                        .HasColumnType("int");

                    b.Property<int>("ZoneTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BulkDiscount = 10,
                            CategoryId = 1,
                            Description = "Sensationell storblommig ratbttros med stora, fyllda, svagt doftande gräddvita blommor med körsbärsröda kanter. Blommar från juni till oktober. Trivs i sol-halvskugga i väldränerad, näringsrik jord",
                            LatinName = "Rosa Taneiglat",
                            Name = "Nostalgi",
                            Number = 10,
                            Price = 699.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 2,
                            BulkDiscount = 10,
                            CategoryId = 1,
                            Description = "Otroligt vacker ros med buskigt växtsätt. Remonterar från sommar till höst med fylllda gula blommor och röda anstrykningar på yttre kronbladen. Friskt sort. Trovs soligt i väldränerad, näringsrik jord.",
                            LatinName = "Rose Lampion",
                            Name = "Lampion",
                            Number = 10,
                            Price = 999.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 3,
                            BulkDiscount = 15,
                            CategoryId = 2,
                            Description = "Vacker lättodlad perenn med hjärformade blommor i rött och vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                            LatinName = "Lamprocapnos specabilis Valentine",
                            Name = "Löjtnatshjärta Valentine",
                            Number = 20,
                            Price = 199.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 4,
                            BulkDiscount = 10,
                            CategoryId = 2,
                            Description = "Vacker lättodlad perenn med hjärformade blommor i rosa och vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.",
                            LatinName = "Lamprocapnos specabilis",
                            Name = "Löjtnadshjärta",
                            Number = 10,
                            Price = 179.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 5,
                            BulkDiscount = 10,
                            CategoryId = 1,
                            Description = "Klasblommig ros med kompakt växtsätt. Remonterar från sommar till höst med fyllda aprikosgula blommor. Medelstark doft. Frisk sort. Trivs i sol-halvskugga i väldränerad näringsrik jord.",
                            Name = "Hansestadt Rostock",
                            Number = 20,
                            Price = 699.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 6,
                            BulkDiscount = 5,
                            CategoryId = 2,
                            Description = "Mattbildande låg timjan som med sitt kompakta växtsätt av små gröna blad och rik blomning i rödviolett blir perfekt marktäckare i stenparti eller som kantväxt. Trivs soligt i väldränerad, mager jord.",
                            LatinName = "Thymus Coccineus",
                            Name = "Purpurtimjan",
                            Number = 3,
                            Price = 59.899999999999999,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 7,
                            BulkDiscount = 10,
                            CategoryId = 5,
                            Description = "Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.",
                            LatinName = "Hudrangea panicilata Sundae fraise",
                            Name = "Vippoprtensia Sundaw fraise",
                            Number = 5,
                            Price = 699.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        },
                        new
                        {
                            Id = 8,
                            BulkDiscount = 10,
                            CategoryId = 5,
                            Description = "Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.",
                            LatinName = "Hudrangea panicilata Living Pink & Rose",
                            Name = "Vipphortensia Living Pink & Rose",
                            Number = 5,
                            Price = 349.0,
                            ZoneFrom = 0,
                            ZoneTo = 0
                        });
                });

            modelBuilder.Entity("SiasGarden.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("SiasGarden.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Klätterros"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Buskros"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rabattros"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Azalea"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Parkrhododendron"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Jord"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Bärbuskar"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Fruktträd"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.ApplicationUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductSubCategory", b =>
                {
                    b.HasOne("SiasGarden.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiasGarden.Models.SubCategory", null)
                        .WithMany()
                        .HasForeignKey("SubCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SiasGarden.Models.Product", b =>
                {
                    b.HasOne("SiasGarden.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SiasGarden.Models.ProductImage", b =>
                {
                    b.HasOne("SiasGarden.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SiasGarden.Models.Product", b =>
                {
                    b.Navigation("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
