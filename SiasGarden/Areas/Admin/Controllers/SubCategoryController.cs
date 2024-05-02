using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;

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
        List<SubCategory> SubCategoryList = _unitOfWork.SubCategory.GetAll().ToList();
        return View(SubCategoryList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(SubCategory subCategory)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.SubCategory.Add(subCategory);
            _unitOfWork.Save();
            TempData["success"] = "Kategorin skapades";
            return RedirectToAction("Index");
        }
        return View();

    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        SubCategory? subCategoryFromDb = _unitOfWork.SubCategory.Get(c => c.Id == id);
        if (subCategoryFromDb == null)
        {
            return NotFound();
        }
        return View(subCategoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(SubCategory subCategory)
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
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        SubCategory? subCategoryFromDb = _unitOfWork.SubCategory.Get(c => c.Id == id);
        if (subCategoryFromDb == null)
        {
            return NotFound();
        }
        return View(subCategoryFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        if (ModelState.IsValid)
        {
            SubCategory subCategory = _unitOfWork.SubCategory.Get(c => c.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.SubCategory.Remove(subCategory);

            _unitOfWork.Save();
            TempData["success"] = "Kategorin togs bort";
            return RedirectToAction("Index");
        }
        return View();

    }
}

