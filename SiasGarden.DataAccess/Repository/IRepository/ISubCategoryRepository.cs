

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface ISubCategoryRepository : IRepository<SubCategory>
{
    void Update(SubCategory subCategory);
  
}
