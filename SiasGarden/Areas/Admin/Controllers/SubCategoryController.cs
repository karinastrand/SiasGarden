using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Collections;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
public class SubCategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public SubCategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<SubCategory> SubCategoryList = _unitOfWork.SubCategory.GetAll(includeProperties:"Category");
        return View(SubCategoryList);
    }
    
    public IActionResult Upsert(int? id)
    {

        SubCategoryVM subCategoryVM = new()
        {

            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            SubCategory = new SubCategory()
        };
        if(id != null|| id>0) 
        {
            subCategoryVM.SubCategory = _unitOfWork.SubCategory.Get(c => c.Id == id, includeProperties: "Category");
          
        }
       
        return View(subCategoryVM);
       
    }
    [HttpPost]
    public IActionResult Upsert(SubCategory subCategory)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.SubCategory.Update(subCategory);
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
        IEnumerable<SubCategory> subCategoryList = _unitOfWork.SubCategory.GetAll(includeProperties:"Category");
        return Json(new { data = subCategoryList });
    }
    public IActionResult Delete(int? id)
    {
        var subCategory = _unitOfWork.SubCategory.Get(u => u.Id == id);
        if (subCategory == null)
        {
            return Json(new { success = false, message = "Fel vid borttagning" });
        }
        _unitOfWork.SubCategory.Remove(subCategory);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Underkategorin har tagits bort" });

    }
    #endregion
}

