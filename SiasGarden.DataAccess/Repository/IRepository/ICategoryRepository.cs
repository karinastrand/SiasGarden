

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
  
}
