using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Repository.IRepository;
namespace SiasGarden.Web.Areas.Customer.Controllers;
[Area("Customer")]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index(int? id)
    {
        ShoppingCart shoppingCart =  _unitOfWork.ShoppingCart.Get(u=>u.Product.Id==id);
        return View(shoppingCart);
    }
}
