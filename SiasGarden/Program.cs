using Microsoft.EntityFrameworkCore;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SiasGarden.Utility;
using SiasGarden.Models;
using Stripe;
using SiasGarden.DataAccess.Data.DbInitializer;
//using Microsoft.AspNetCore.Authentication.Facebook;
//using Microsoft.Extensions.Configuration;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});



//if (builder.Environment.IsProduction())
//{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
//}

builder.Services.AddAuthentication().AddFacebook(options =>
{
    IConfigurationSection Facebook =
    builder.Configuration.GetSection("Facebook");
    options.AppId = Facebook["AppId"];
    options.AppSecret = Facebook["AppSecret"];

});
builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection Google =
    builder.Configuration.GetSection("Google");
    options.ClientId = Google["ClientId"];
    options.ClientSecret = Google["ClientSecret"];

});

builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
{
    IConfigurationSection Microsoft =
    builder.Configuration.GetSection("Microsoft");
    options.ClientId = Microsoft["ClientId"];
    options.ClientSecret = Microsoft["ClientSecret"];

});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options=>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
SeedDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope=app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initialize();
    }
}
