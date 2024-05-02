using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SiasGarden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BulkDiscount = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSubCategories",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSubCategories", x => new { x.ProductsId, x.SubCategoriesId });
                    table.ForeignKey(
                        name: "FK_ProductsSubCategories_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSubCategories_SubCategories_SubCategoriesId",
                        column: x => x.SubCategoriesId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "Rosor" },
                    { 2, null, 2, "Perenner" },
                    { 3, null, 3, "Fröer" },
                    { 4, null, 4, "Rhododendron" },
                    { 5, null, 5, "Buskar" },
                    { 6, null, 6, "Träd" },
                    { 7, null, 7, "Tillbehör" },
                    { 8, null, 8, "Redskap" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Klätterros" },
                    { 2, null, "Buskros" },
                    { 3, null, "Rabattros" },
                    { 4, null, "Azalea" },
                    { 5, null, "Parkrhododendron" },
                    { 6, null, "Jord" },
                    { 7, null, "Bärbuskar" },
                    { 8, null, "Fruktträd" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BulkDiscount", "CategoryId", "Description", "LatinName", "Name", "Number", "Price" },
                values: new object[,]
                {
                    { 1, 10, 1, "Sensationell storblommig ratbttros med stora, fyllda, svagt doftande gräddvita blommor med körsbärsröda kanter. Blommar från juni till oktober. Trivs i sol-halvskugga i väldränerad, näringsrik jord", "Rosa Taneiglat", "Nostalgi", 10, 699.0 },
                    { 2, 10, 1, "Otroligt vacker ros med buskigt växtsätt. Remonterar från sommar till höst med fylllda gula blommor och röda anstrykningar på yttre kronbladen. Friskt sort. Trovs soligt i väldränerad, näringsrik jord.", "Rose Lampion", "Lampion", 10, 999.0 },
                    { 3, 15, 2, "Vacker lättodlad perenn med hjärformade blommor i rött och vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.", "Lamprocapnos specabilis Valentine", "Löjtnatshjärta Valentine", 20, 199.0 },
                    { 4, 10, 2, "Vacker lättodlad perenn med hjärformade blommor i rosa och vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.", "Lamprocapnos specabilis", "Löjtnadshjärta", 10, 179.0 },
                    { 5, 10, 1, "Klasblommig ros med kompakt växtsätt. Remonterar från sommar till höst med fyllda aprikosgula blommor. Medelstark doft. Frisk sort. Trivs i sol-halvskugga i väldränerad näringsrik jord.", null, "Hansestadt Rostock", 20, 699.0 },
                    { 6, 5, 2, "Mattbildande låg timjan som med sitt kompakta växtsätt av små gröna blad och rik blomning i rödviolett blir perfekt marktäckare i stenparti eller som kantväxt. Trivs soligt i väldränerad, mager jord.", "Thymus Coccineus", "Purpurtimjan", 3, 59.899999999999999 },
                    { 7, 10, 5, "Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.", "Hudrangea panicilata Sundae fraise", "Vippoprtensia Sundaw fraise", 5, 699.0 },
                    { 8, 10, 5, "Buske med vackra konformade blomklasar som ändrar färg från vitt till rosa när de åldras. Trivs i sol-halvskugga i näringsrik, väldränderad och fuktighetshållande jord.", "Hudrangea panicilata Living Pink & Rose", "Vipphortensia Living Pink & Rose", 5, 349.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSubCategories_SubCategoriesId",
                table: "ProductsSubCategories",
                column: "SubCategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductsSubCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
