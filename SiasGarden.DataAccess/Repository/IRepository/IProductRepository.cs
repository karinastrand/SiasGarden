

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
  
}
