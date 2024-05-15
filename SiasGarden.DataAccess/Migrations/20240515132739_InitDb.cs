using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SiasGarden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTotal = table.Column<double>(type: "float", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Light = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: false),
                    ZoneTo = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rosor" },
                    { 2, "Perenner" },
                    { 3, "Klängväxter" },
                    { 4, "Buskar" },
                    { 5, "Träd" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Kryddväxt" },
                    { 2, 1, "Jordröksväxter" },
                    { 3, 2, "Buskrosor" },
                    { 4, 2, "Rabattrosor" },
                    { 5, 3, "Klematis" },
                    { 6, 3, "Kaprifol" },
                    { 7, 4, "Prydnadsbuskar" },
                    { 8, 4, "Bärbuskar" },
                    { 9, 5, "Prydnadsträd" },
                    { 10, 5, "Fruktträd" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Height", "LatinName", "Light", "Name", "Price", "SubCategoryId", "ZoneTo" },
                values: new object[,]
                {
                    { 1, "Blommar rikligt med mörkt blåviolett blommor och har ett kompakt, lågväxande växtsätt. Utvecklas bäst i full sol på väldränerad jord. En av de härdigaste sorterna.", 60, "Lavendula augustifolia 'Hidcote'", "Sol", "Lavendel 'Hidcote'", 59.0, 1, 5 },
                    { 2, "Blomvillig och lättskött perenn för soliga lägen med djupt, mörklila blomax på purpurfärgade stjälkar. Blommar rikligt och länge.", 60, "Salvia nemorosa 'Caradonna'", "Sol", "Stäppsalvia 'Caradonna'", 49.0, 1, 3 },
                    { 3, "Vacker lättodlad perenn med hjärformade blommor i rött och vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.", 40, "Lamprocapnos specabilis 'Valentine'", "Sol till halvskugga", "Löjtnatshjärta 'Valentine'", 199.0, 2, 3 },
                    { 4, "Vacker, lättodlad perenn med hjärtformade blommor i vitt. Klipp ner efter blomning för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge. för chans till ytterligare en blomning senare på sommaren. Finast i halvskuggigt, skyddat läge.", 40, "Lamprocapnos spectabilis 'Alba'", "Sol till halvskugga", "Löjtnadshjärta 'Alba'", 84.0, 2, 9 },
                    { 5, "Klasblommig ros med kompakt växtsätt. Remonterar från sommar till höst med fyllda aprikosgula blommor. Medelstark doft. Frisk sort. Trivs i sol-halvskugga i väldränerad näringsrik jord.", 80, null, "Sol till halvskugga", "Hansestadt Rostock", 289.0, 4, 4 },
                    { 6, "Sensationell storblommig rabattros med stora, fyllda, svagt doftande gräddvita blommor med körsbärsröda kanter. Blommar från juni till oktober. Trivs i sol-halvskugga i väldränerad, näringsrik jord.", 70, null, "Sol", "Nostalgi", 299.0, 4, 4 },
                    { 7, "Buskros med upprätt och tätt växtsätt. Bladverket är läderartat, mörkgrönt och glänsande. Blommar med tätt fyllda, mörkt sammetsröda blommor som har en medelstark doft. Riklig blomning nästan oavbrutet från juni och in på senhösten. Mycket användbar då den kan planteras i grupper, tillsammans med perenner, som kantväxt och häckväxt, blir även fin i kruka. Trivs bäst i soliga lägen i näringsrik jord.", 150, null, "Sol", "Isabel Renaissance", 345.0, 3, 3 },
                    { 8, "Buskros med ett kraftigt, buskigt växtsätt. Bladverket är friskt, grönt där all nytillväxt är vackert röd. Praktfull blomning med klasar av gula, fyllda blommor som ljusnar under blomningen till en härlig svavelgul nyans. Doftar angenämt och tål regn bra. Är en frisk ros som är motståndskraftig mot olika svampsjukdomar", 100, null, "Sol", "High Voltage", 345.0, 3, 5 },
                    { 9, "Vacker hybrid som blommar länge från sommar till tidig höst med sammetsröda blommor mot mörkgrönt bladverk. Trivs i sol–halvskugga i fukthållande, väldränerad jord. Beskärningsgrupp 2.", 200, null, "Sol-halvskugga", "Clematis Hisako", 350.0, 5, 6 },
                    { 10, "Storblommig klematis med läckert körsbärsröda blommor. Blommar under en lång tid. Blomrik och tålig. För soliga till halvskuggiga lägen. Beskärningsgrupp 3.", 400, null, "Sol-halvskugga", "Clematis 'Ville de Lyon'", 199.0, 5, 5 },
                    { 11, "Lonicera heckrottii 'Goldflame' är en lättodlad och vacker kaprifol. Sorten är kraftfull och snabbväxande. Blommar med rörformiga, rosa/rödrosa blommor under juni - augusti. På hösten kommer dekorativa oätliga röda bär. Bladen är ovala, medelstora, blågröna och ger fin gul höstfärg. Doften är frisk, intensivt och fantastisk. Sorten är mycket tålig och ger riklig blomning på en solig plats. Älskas av fjärilar, bin och humlor", 350, "Lonicera heckrottii 'Goldflame'", "Sol-halvskugga", "Kaprifol 'Goldflame'", 28.0, 6, 3 },
                    { 12, "Färggrann sort med kraftigt, slingrande och täckande växtsätt med frodigt blågrönt bladverk. Blommar rikligt med doftande, brandgula blommor varvat med bronsgula knoppar. Bildar röda, oätliga bär. Trivs i sol till halvskugga på väldränerad, näringsrik jord.3.", 450, "Lonicera x tellmanniana'", "Sol-halvskugga", "Tellmannskaprifol", 199.0, 6, 4 },
                    { 13, "Elegant, storblommig sort med buskigt, upprätt växtsätt. Blommar länge med gräddvita, fylliga klasar som skiftar till mörkrosa och avslutas i mörkrosa nyanser. Trivs i sol till halvskugga på mullrik, lätt kemiskt sur jord.", 250, "Hydrangea paniculata 'Vanille-Fraise'", "Sol-halvskugga", "Vipphortensia 'Vanille-Fraise'", 299.0, 7, 5 },
                    { 14, "Syrenbuddleja kallas även för fjärilsbuske, då den är känd för att locka till sig fjärilar. Fungerar att planteras både som solitär och i grupp. Anses vara mindre smaklig för rådjur.", 199, "Buddleja davidii 'Royal Red'", null, "Syrenbuddleja 'Royal Red'", 340.0, 7, 3 },
                    { 15, "Kraftig med lätt överhängande grenar och rik skörd av svarta bär i rejäla klasar. Härdig och motståndskraftig mot mjöldagg. Trivs i sol-halvskugga i väldränerad, näringsrik, fuktighetshållande jord.", 150, "Ribes nigrum 'Öjebyn'", "Sol-halvskugga", "Svarta vinbär 'Öjebyn'", 149.0, 8, 6 },
                    { 16, "Frodig slingerväxt som får gräddvita, doftande blommor och små äggformade, bruna frukter med hårigt skal och grönt, sötsyrligt fruktkött. Självfertil men gynnas av samplantering med hanplanta. Ger bäst skörd på en varm solig och skyddad plats, gärna i växthus.", 400, "Actinidia deliciosa 'Jenny'", "Sol", "Kivi 'Jenny'", 379.0, 8, 1 },
                    { 17, "Medelstarkväxande, populär sort som ger gulgröna äpplen med röd solsida. Har ett fint, fast fruktkött med en sötsyrlig smak. Frisk sort. Kan lagras fram till jul. Pollineras av bl. a 'Alice' och 'Ingrid Marie'. Bäst skörd i soligt läge.", 500, "Malus domestica 'Aroma'", "Sol-halvskugga", "Aroma", 520.0, 10, 4 },
                    { 18, "Svagväxande, populär sort som får stora, medeltidiga skördar med stora, rödgula frukter med ljusviolett daggig yta. Fast fruktkött med söt smak. Självfertil. Ger bäst skörd i soligt läge på väldränerad och näringsrik jord.", 450, "Prunus domestica 'Victoria'", "Sol-halvskugga", "Victoria", 799.0, 10, 3 },
                    { 19, "Odlingsvärt litet träd med tät, rundad krona. Vackert ljusgrönt bladverk med vitbrokiga skott som har rosatonad anstrykning på nytillväxten. Trivs i skyddat, soligt läge på väldränerad, näringsrik jord.", 250, "Salix integra 'Hakuro-nishiki'", "Sol", "Eukalyptusvide 'Hakuro-nishiki'", 799.0, 9, 2 },
                    { 20, "Färggrann sort med kraftigt, slingrande och täckande växtsätt med frodigt blågrönt bladverk. Blommar rikligt med doftande, brandgula blommor varvat med bronsgula knoppar. Bildar röda, oätliga bär. Trivs i sol till halvskugga på väldränerad, näringsrik jord.3.", 450, "Salix caprea 'Kilmarnock'", "Sol", "Hängsälg 'Kilmarnock'", 100.0, 9, 6 },
                    { 21, "Med det nätta formatet och det hängande växtsättet passar hängsälgen i kruka väldigt fint men också mitt i en större plantering för att skapa höjd. Fördelen med att den är uppstammad är att man kan plantera olika sommarblommor eller perenner under den. Samma sak när hängsälg står i rabatten, ett litet träd med videkissar blir perfekt med blommor nedanför.", 120, "Hydrangea paniculata 'Sundae-Fraise'", "Sol-halvskugga", "Vipphortensia 'Sundae-Fraise'", 450.0, 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "\\images\\products\\product-1\\startbild.jpg", 1 },
                    { 2, "\\images\\products\\product-2\\startbild.jpg.jpg", 2 },
                    { 3, "\\images\\products\\product-3\\startbild.jpg", 3 },
                    { 4, "\\images\\products\\product-4\\startbild.jpg", 4 },
                    { 5, "\\images\\products\\product-5\\startbild.jpg", 5 },
                    { 6, "\\images\\products\\product-6\\startbild.jpg", 6 },
                    { 7, "\\images\\products\\product-7\\startbild.jpg", 7 },
                    { 8, "\\images\\products\\product-8\\startbild.jpg", 8 },
                    { 9, "\\images\\products\\product-9\\startbild.jpg", 9 },
                    { 10, "\\images\\products\\product-10\\startbild.jpg", 10 },
                    { 11, "\\images\\products\\product-11\\startbild.jpg", 11 },
                    { 12, "\\images\\products\\product-12\\startbild.jpg", 12 },
                    { 13, "\\images\\products\\product-13\\startbild.jpg", 13 },
                    { 14, "\\images\\products\\product-14\\startbild.jpg", 14 },
                    { 15, "\\images\\products\\product-15\\startbild.jpg", 15 },
                    { 16, "\\images\\products\\product-16\\startbild.jpg", 16 },
                    { 17, "\\images\\products\\product-17\\startbild.jpg", 17 },
                    { 18, "\\images\\products\\product-18\\startbild.jpg", 18 },
                    { 19, "\\images\\products\\product-19\\startbild.jpg", 19 },
                    { 20, "\\images\\products\\product-20\\startbild.jpg", 20 },
                    { 21, "\\images\\products\\product-21\\startbild.jpg", 21 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ApplicationUserId",
                table: "ShoppingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
