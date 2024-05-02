

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface IProductImageRepository : IRepository<ProductImage>
{
    void Update(ProductImage productImage);
  
}
