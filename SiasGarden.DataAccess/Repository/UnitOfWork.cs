
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;

namespace SiasGarden.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }

    private ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category=new CategoryRepository(_db);
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}
