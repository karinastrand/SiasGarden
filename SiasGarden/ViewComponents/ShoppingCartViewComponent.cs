using Microsoft.AspNetCore.Mvc;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Utility;
using System.Security.Claims;

namespace SiasGarden.Web.ViewComponents;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;
    public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
           
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {

        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            if(HttpContext.Session.GetInt32(SD.SessionCart)==null)
            {
                var CartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId.Value);
                int numberOfProductsInCart = 0;
                foreach (var cart in CartList) 
                {
                    numberOfProductsInCart += cart.Count;
                }
                HttpContext.Session.SetInt32(SD.SessionCart, numberOfProductsInCart);
            }
      
            return View(HttpContext.Session.GetInt32(SD.SessionCart));
        }
        else
        {
            HttpContext.Session.Clear();
            return View(0);
        }
    }
}
