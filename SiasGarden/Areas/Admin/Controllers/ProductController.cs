using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiasGarden.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;
using SiasGarden.Utility;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
[Authorize(Roles = SD.Role_Employee)]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;   
    }
    public IActionResult Index()
    {
        List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
        
        return View(ProductList);
    }
    public IActionResult Upsert(int? id)
    {
       

        ProductVM productVM = new ProductVM()
        {
            Product=new Product(),
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
        };
        if (id != null && id > 0)
        {
            productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
       
        }
    
        return View(productVM);
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath =_webHostEnvironment.WebRootPath;
            if(file !=null) 
            {
                string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                string productPath=Path.Combine(wwwRootPath, @"startimages\products");

                if(!string.IsNullOrEmpty(productVM.Product.StartImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.StartImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath)) 
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.StartImageUrl = @"\startimages\products\"+fileName;
            }
            if(productVM.Product.Id==0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }
           
            _unitOfWork.Save();
            TempData["success"] = "Produkten skapades/uppdaterades";
            return RedirectToAction("Index");
        }
        else
        {

            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
           
            return View(productVM);
        }
     }
  
  

    #region APICALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = productList });
    }
  
    public IActionResult Delete(int? id)
    {
       var product=_unitOfWork.Product.Get(u=>u.Id == id);
        if(product == null)
        {
            return Json(new {success = false, message="Fel vid borttagning"});
        }
        if (!string.IsNullOrEmpty(product.StartImageUrl))
        {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.StartImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Produkten har tagits bort" });
        
    }
    #endregion

}
