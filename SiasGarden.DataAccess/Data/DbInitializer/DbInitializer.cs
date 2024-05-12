

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiasGarden.Models;
using SiasGarden.Utility;

namespace SiasGarden.DataAccess.Data.DbInitializer;

public class DbInitializer: IDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public void Initialize()
    {
        try
        {
            if(_db.Database.GetPendingMigrations().Count() > 0) 
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {

          
        }
        _db.Database.EnsureCreated();

        if(!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) 
        {
        
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "sias@mail.se",
                Email = "sias@mail.se",
                FirstName = "Sia",
                LastName = "Admin",
                PhoneNumber = "070-123321",
                StreetAddress = "Testvägen 3",
                PostalCode = "123 45",
                City = "Teststad"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "sias@mail.se");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
        return;
        
    }
}
