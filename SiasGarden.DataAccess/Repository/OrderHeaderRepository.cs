

using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository;

internal class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private ApplicationDbContext _db;

    public OrderHeaderRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(OrderHeader orderHeader)
    {
        _db.Update(orderHeader);
    }

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderHeaderFromDb=_db.OrderHeaders.FirstOrDefault(u=>u.Id==id);
        if (orderHeaderFromDb!=null) 
        {
            orderHeaderFromDb.OrderStatus = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orderHeaderFromDb.PaymentStatus = paymentStatus;
            }
   
        }
    }

    public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
    {
        var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
        if (!string.IsNullOrEmpty(sessionId))
        {
            orderHeaderFromDb.SessionId = sessionId;
        }
        if(!string.IsNullOrEmpty(paymentIntentId)) 
        {
            orderHeaderFromDb.PaymentIntentId = paymentIntentId;
            orderHeaderFromDb.PaymentDate = DateTime.Now;   
        }
    }
}
