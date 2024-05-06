using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SiasGarden.Web.Areas.Customer.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(productList);
    }
    public IActionResult Details(int productId)
    {
        Product product=_unitOfWork.Product.Get(u=>u.Id == productId, includeProperties:"Category");
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
        }
        else
        {
            oldShoppingCart.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(oldShoppingCart);
        }
       
        _unitOfWork.Save();

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
