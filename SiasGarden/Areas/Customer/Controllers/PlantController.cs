using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;
using SiasGarden.Models.ViewModels;
using SiasGarden.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace SiasGarden.Web.Areas.Customer.Controllers;
[Area("Customer")]

public class PlantController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PlantController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

   
    public IActionResult Index(string ? selectedCategory)
    {
       
        IEnumerable<Product> productList = (_unitOfWork.Product.GetAll(includeProperties: "SubCategory,ProductImages")).OrderBy(u => u.Name);
        
        IEnumerable<Product> selectedProducts = productList;
        if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory!="Alla")
        {
           
            selectedProducts = productList.Where(u=>u.SubCategory.Category.Name==selectedCategory);
        }
        else
        {
            selectedCategory = "Alla";
           // selectedProducts = productList.Where(u => u.SubCategory.Category.Name == selectedCategory);
        }
        IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
        ProductVM2 productVM2 = new ProductVM2()
        { 
            Products = selectedProducts,
            Categorys = categoryList,
            SelectedCategory=selectedCategory
        };
       
        return View(productVM2);
    }
    public IActionResult Details(int productId)
    {
        Product product=_unitOfWork.Product.Get(u=>u.Id == productId, includeProperties:"SubCategory,ProductImages");
        ShoppingCart shoppingCart = new ShoppingCart()
        {
            Product = product,
            Count = 1,
            ProductId=productId
        };
       

        return View(shoppingCart);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart) 
    {
        var claimsIdentity=(ClaimsIdentity)User.Identity;
        var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        ShoppingCart oldShoppingCart=_unitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId==userId && u.ProductId==shoppingCart.ProductId);
        
        if (oldShoppingCart == null)
        {
            shoppingCart.ApplicationUserId = userId;
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
            

        }
        else
        {
            oldShoppingCart.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(oldShoppingCart);
            _unitOfWork.Save();
        }
        var CartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId);
        int numberOfProductsInCart = 0;
        foreach (var cart in CartList)
        {
            numberOfProductsInCart += cart.Count;
        }
        HttpContext.Session.SetInt32(SD.SessionCart, numberOfProductsInCart);
        TempData["success"] = "Kundvagnen har uppdaterats";

        return RedirectToAction("Index");  
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
