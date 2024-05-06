

using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository.IRepository;

public interface IApplicationUserRepository : IRepository<ApplicationUser>
{
    void Update(ApplicationUser application);
  
}
