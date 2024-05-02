

using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository;

internal class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
{
    private ApplicationDbContext _db;

    public SubCategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(SubCategory subCategory)
    {
       _db.Update(subCategory);
    }
}
