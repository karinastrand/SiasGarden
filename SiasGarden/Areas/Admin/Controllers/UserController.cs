using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiasGarden.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;
using SiasGarden.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Hosting;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]


public class UserController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext db)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager; 
        _roleManager = roleManager; 
    }
    public IActionResult Index()
    {
       
        return View();
    }

    public IActionResult Edit(string userId)
    {

        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        applicationUser.Role = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();
        UserVM userVM = new UserVM()
        {
            ApplicationUser=applicationUser,
            
            RoleList = _roleManager.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
          
        };
        

        return View(userVM);
    }
    [HttpPost]
    public IActionResult Edit(UserVM userVM)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser applicationUserFromDb=_unitOfWork.ApplicationUser.Get(u=>u.Id==userVM.ApplicationUser.Id);
            applicationUserFromDb.FirstName=userVM.ApplicationUser.FirstName;
            applicationUserFromDb.LastName=userVM.ApplicationUser.LastName;
            applicationUserFromDb.Email = userVM.ApplicationUser.Email;
            applicationUserFromDb.PhoneNumber = userVM.ApplicationUser.PhoneNumber;
            applicationUserFromDb.UserName = userVM.ApplicationUser.Email;
            applicationUserFromDb.StreetAddress = userVM.ApplicationUser.StreetAddress;
            applicationUserFromDb.City = userVM.ApplicationUser.City;
            applicationUserFromDb.PostalCode = userVM.ApplicationUser.PostalCode;
            applicationUserFromDb.StreetAddress=userVM.ApplicationUser.StreetAddress;
           
            var oldUserRole=_userManager.GetRolesAsync(applicationUserFromDb).GetAwaiter().GetResult().FirstOrDefault();
            var newUserRole = _roleManager.Roles.Where(u=>u.Id==userVM.ApplicationUser.Role).FirstOrDefault().ToString();
            _userManager.RemoveFromRoleAsync(userVM.ApplicationUser,oldUserRole).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(userVM.ApplicationUser,newUserRole).GetAwaiter().GetResult();
            
           _unitOfWork.ApplicationUser.Update(applicationUserFromDb);
           _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        else
        {

            userVM.RoleList = _roleManager.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            
            return View(userVM);
        }
    }


    #region APICALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<ApplicationUser> userList = _unitOfWork.ApplicationUser.GetAll().ToList();
       
        foreach (var user in userList)
        {
          
          
            user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            
         
        }
        return Json(new { data = userList });
    }
    public IActionResult Delete(int? id)
    {
       
        return Json(new { success = true, message = "Produkten har tagits bort" });

    }
    #endregion

}
