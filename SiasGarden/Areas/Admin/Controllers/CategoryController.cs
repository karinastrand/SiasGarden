using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SiasGarden.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles =SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryController(IUnitOfWork unitOfWork)
    {
        
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
       
       
        List<Category> CategoryList= _unitOfWork.Category.GetAll().ToList();
        return View(CategoryList);
    }
    
    public IActionResult Upsert(int? id)
    {
       Category categoryFromDb=new Category();
        if (id !=null) 
        {
            categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
        }
      
        
        return View(categoryFromDb);
    }
    [HttpPost]
    public IActionResult Upsert(Category category)
    {
        if (ModelState.IsValid)
        {
            if(category.Id==0)
            {
                _unitOfWork.Category.Add(category);
            }
            else
            {
                _unitOfWork.Category.Update(category);
            }
            
            _unitOfWork.Save();
            TempData["success"] = "Kategorin uppdaterades";
            return RedirectToAction("Index");
        }
        return View();

    }
    
    #region APICALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Category> CategoryList = _unitOfWork.Category.GetAll();
        return Json(new { data = CategoryList });
    }
    public IActionResult Delete(int? id)
    {
        var category = _unitOfWork.Category.Get(u => u.Id == id);
        if (category == null)
        {
            return Json(new { success = false, message = "Fel vid borttagning" });
        }
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Kategorin har tagits bort" });

    }
    #endregion
}
