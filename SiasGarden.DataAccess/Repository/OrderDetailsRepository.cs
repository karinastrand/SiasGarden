

using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository;

internal class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private ApplicationDbContext _db;

    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(OrderDetail orderDetail)
    {
       _db.Update(orderDetail);
    }
}
