using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;
using SiasGarden.Models.ViewModels;
using SiasGarden.Utility;
using Stripe;
using Stripe.Issuing;
using System.Diagnostics;
using System.Security.Claims;

namespace SiasGarden.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class OrderController : Controller
{
    public readonly IUnitOfWork _unitOfWork;
    [BindProperty]
    public OrderVM OrderVM { get; set; }
    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Details(int orderId)
    {
        OrderVM = new OrderVM()
        {
           OrderHeader=_unitOfWork.OrderHeader.Get(u=>u.Id==orderId,includeProperties:"ApplicationUser"),
           OrderDetail=_unitOfWork.OrderDetail.GetAll(u=>u.OrderHeaderId==orderId, includeProperties:"Product")
        };
        return View(OrderVM);
    }

    [HttpPost]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public IActionResult StartProcessing()
    {
       
        _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id,SD.StatusInProcess);
        _unitOfWork.Save();
        TempData["Success"] = "Orderstatus har satts till 'På gång'.";
        return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id }); ;
    }


    [HttpPost]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public IActionResult ShipOrder()
    {
        var orderHeader=_unitOfWork.OrderHeader.Get(u=>u.Id==OrderVM.OrderHeader.Id);
        orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
        orderHeader.Carrier=OrderVM.OrderHeader.Carrier;
        orderHeader.OrderStatus=SD.StatusShipped;
        orderHeader.ShippingDate=DateTime.Now;

        _unitOfWork.OrderHeader.Update(orderHeader);
        _unitOfWork.Save();
        TempData["Success"] = "Orderstatus har satts till 'Skickad'.";
        return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id }); ;
    }

    [HttpPost]
    [Authorize(Roles=SD.Role_Admin+","+SD.Role_Employee)]
    public IActionResult UpdateOrderDetails()
    {
        OrderHeader orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
        orderHeaderFromDb.FirstName = OrderVM.OrderHeader.FirstName;
        orderHeaderFromDb.LastName = OrderVM.OrderHeader.LastName;
        orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
        orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
        orderHeaderFromDb.City  = OrderVM.OrderHeader.City;
        orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
        if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
        {
            orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
        }
        if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
        {
            orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
        }
        _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
        _unitOfWork.Save();
        TempData["Success"] = "Orderdetaljer har uppdaterats";
        return RedirectToAction(nameof(Details),new { orderId = orderHeaderFromDb.Id });
    }
    public IActionResult CancelOrder()
    {
        OrderHeader orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
        if (orderHeaderFromDb.PaymentStatus == SD.PaymentStatusApproved)
        {

            var options = new RefundCreateOptions
            {

                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeaderFromDb.PaymentIntentId
            };
            var service = new RefundService();
            Refund refund = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.Id, SD.StatusCancelled, SD.StatusRefunded);
        }
        else
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.Id, SD.StatusCancelled, SD.StatusCancelled);
        }
     
        _unitOfWork.Save();
        TempData["Success"] = "Ordern har annulerats";
        return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
    }


    #region APICALLS

    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<OrderHeader> orderHeaderList;
        if (User.IsInRole(SD.Role_Admin)||User.IsInRole(SD.Role_Employee))
        {
            orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
        }
        else
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            orderHeaderList = _unitOfWork.OrderHeader.GetAll(u=>u.ApplicationUser.Id==userId,includeProperties: "ApplicationUser").ToList();
        }
        switch (status)
        {
           
            case "inprocess":
                orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == SD.StatusInProcess);
                break;
            case "completed":
                orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == SD.StatusShipped);
                break;
            case "approved":
                orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == SD.StatusApproved);
                break;
            
        }
        return Json(new { data = orderHeaderList });
    }

   
    #endregion

}
