using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiasGarden.Models.ViewModels;

namespace SiasGarden.Areas.Admin.Controllers;
[Area("Admin")]
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
  
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product? productFromDb = _unitOfWork.Product.Get(c => c.Id == id);
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        if (ModelState.IsValid)
        {
            Product product = _unitOfWork.Product.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);

            _unitOfWork.Save();
            TempData["success"] = "Kategorin togs bort";
            return RedirectToAction("Index");
        }
        return View();

    }

    #region APICALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = productList });
    }
    #endregion

}
