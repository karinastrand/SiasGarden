

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail orderDetail);
  
}
