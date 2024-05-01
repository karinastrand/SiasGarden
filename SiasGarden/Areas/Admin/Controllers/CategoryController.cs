using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
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
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]  
    public IActionResult Create(Category category)
    {
        if(ModelState.IsValid)
        { 
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Kategorin skapades";
            return RedirectToAction("Index");
        }
        return View();
           
    }
    public IActionResult Edit(int? id)
    {
        if (id==null||id==0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
        if (categoryFromDb==null) 
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
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
        Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost ,ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        if (ModelState.IsValid)
        {
            Category category=_unitOfWork.Category.Get(c => c.Id==id);
            if (category==null) 
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
         
            _unitOfWork.Save();
            TempData["success"] = "Kategorin togs bort";
            return RedirectToAction("Index");
        }
        return View();

    }
}
