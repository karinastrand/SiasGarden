using Microsoft.AspNetCore.Mvc;
using SiasGarden.Models;
using SiasGarden.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using SiasGarden.Models.ViewModels;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using SiasGarden.Utility;
using Stripe.Checkout;
using Microsoft.CodeAnalysis.CSharp;
namespace SiasGarden.Web.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    [BindProperty]
    public ShoppingCartVM ShoppingCartVM { get; set; }
   


    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index(int? id)
    {
        var claimsIdentity=(ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
            OrderHeader=new()
        };
        foreach (var item in shoppingCartVM.ShoppingCartList)
        {
            shoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Product.Price);
        }
        
        return View(shoppingCartVM);
    }
   
    public IActionResult Summary()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,includeProperties:"Product"),
            OrderHeader = new()
        };
        shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        
        shoppingCartVM.OrderHeader.FirstName=shoppingCartVM.OrderHeader.ApplicationUser.FirstName;
        shoppingCartVM.OrderHeader.LastName = shoppingCartVM.OrderHeader.ApplicationUser.LastName;
        shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        shoppingCartVM.OrderHeader.StreetAddress = shoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
        shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
        shoppingCartVM.OrderHeader.PostalCode = shoppingCartVM.OrderHeader.ApplicationUser.PostalCode;



        foreach (var item in shoppingCartVM.ShoppingCartList)
        {
            shoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Product.Price);
        }

        return View(shoppingCartVM);
    }
    [HttpPost]
    [ActionName("Summary")]
	public IActionResult SummaryPOST()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;
		var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		ShoppingCartVM.OrderHeader.OrderDate=System.DateTime.Now;
        ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");		
		ShoppingCartVM.OrderHeader.ApplicationUserId=userId;
		
        ShoppingCartVM.OrderHeader.OrderStatus=SD.StatusPending;
        ShoppingCartVM.OrderHeader.PaymentStatus=SD.PaymentStatusPending;



		foreach (var item in ShoppingCartVM.ShoppingCartList)
		{
			ShoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Product.Price);
		}
        _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
        _unitOfWork.Save();

        foreach (var item in ShoppingCartVM.ShoppingCartList)
        {
            OrderDetail orderDetail = new()
            {
                ProductId = item.ProductId,
                OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                Price= item.Product.Price,
                Count= item.Count
              
            };
            _unitOfWork.OrderDetail.Add(orderDetail);
            _unitOfWork.Save();
        }
        var domain = Request.Scheme+"://"+Request.Host.Value+"/";
        var options = new SessionCreateOptions
        {
            SuccessUrl = domain + $"Customer/ShoppingCart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
            CancelUrl = domain + "Customer/ShoppingCart/Index",
            LineItems = new List<SessionLineItemOptions>(),

            Mode = "payment",
        };

        foreach (var item in ShoppingCartVM.ShoppingCartList)
        {
            var sessionLIneItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Product.Price * 100),
                    Currency = "SEK",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Name
                    }
                },
                Quantity = item.Count
            };
            options.LineItems.Add(sessionLIneItem);
        }
        var service = new SessionService();
        Session session =service.Create(options);
        _unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();
        Response.Headers.Add("Location",session.Url);
        return new StatusCodeResult(303);
        return RedirectToAction(nameof(OrderConfirmation),new { id=ShoppingCartVM.OrderHeader.Id });
      
    }
    
    public IActionResult OrderConfirmation(int id)
    {
        OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u=>u.Id==id, includeProperties:"ApplicationUser");

        var service =new SessionService();
        Session session = service.Get(orderHeader.SessionId);
        if(session.PaymentStatus.ToLower()=="paid")
        {
            _unitOfWork.OrderHeader.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved,SD.PaymentStatusApproved);
            _unitOfWork.Save(); 
        }
        HttpContext.Session.Clear();
        List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart
            .GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
        _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
        _unitOfWork.Save();
        return View(id);
    }

    public IActionResult Plus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
        cartFromDb.Count += 1;
        _unitOfWork.ShoppingCart.Update(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }

    public IActionResult Minus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked:true);
        if(cartFromDb.Count >1)
        { 
            cartFromDb.Count -= 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
           
        }
        else
        {
            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);

            _unitOfWork.ShoppingCart.Remove(cartFromDb);
        }
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    public IActionResult Remove(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked:true) ;
        HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);

        _unitOfWork.ShoppingCart.Remove(cartFromDb);

        _unitOfWork.Save();
        return RedirectToAction("Index");
    }

    

}
