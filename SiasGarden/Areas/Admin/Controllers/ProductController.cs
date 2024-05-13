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
        List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties:"Category,SubCategory").ToList();
        
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
            }),
            SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
        };
        if (id != null && id > 0)
        {
            productVM.Product = _unitOfWork.Product.Get(u => u.Id == id,includeProperties:"ProductImages");
       
        }
    
        return View(productVM);
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
    {
        if (ModelState.IsValid)
        {
            if (productVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }
            _unitOfWork.Save();

            string wwwRootPath =_webHostEnvironment.WebRootPath;
            if(files !=null) 
            {

                foreach (IFormFile file in files) 
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath=@"images\products\product-"+productVM.Product.Id;
                    string finalPath = Path.Combine(wwwRootPath, productPath);
                    if(!Directory.Exists(finalPath)) 
                    {
                        Directory.CreateDirectory(finalPath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductImage productImage = new ProductImage()
                    {
                        ImageUrl = @"\" + productPath + @"\" + fileName,
                        ProductId= productVM.Product.Id, 
                    };
                    if(productVM.Product.ProductImages==null)
                    {
                        productVM.Product.ProductImages = new List<ProductImage>();
                    }
                    productVM.Product.ProductImages.Add(productImage);  
                    
                }
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();

              
            }
           
           
         
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
            productVM.SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVM);
        }
     }

    public IActionResult DeleteImage(int imageId)
    {
        var imgToBeDeleted = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
        if (imgToBeDeleted != null)
        {
            if (!string.IsNullOrEmpty(imgToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imgToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.ProductImage.Remove(imgToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Bilden togs bort";

        }
        return RedirectToAction(nameof(Upsert), new { id = imgToBeDeleted.ProductId });
    }

    #region APICALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,SubCategory").ToList();
        return Json(new { data = productList });
    }
  
    public IActionResult Delete(int? id)
    {
       var product=_unitOfWork.Product.Get(u=>u.Id == id);
        if(product == null)
        {
            return Json(new {success = false, message="Fel vid borttagning"});
        }
      
        string productPath = @"images\products\product-" + id;
        string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);
        if (Directory.Exists(finalPath))
        {
            string[] filePaths = Directory.GetFiles(finalPath);
            foreach (string filePath in filePaths) 
            {
               System.IO.File.Delete(filePath);
            }
            Directory.Delete(finalPath);
        }

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Produkten har tagits bort" });
        
    }

   
    #endregion

}
