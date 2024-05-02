

using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository;

internal class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    private ApplicationDbContext _db;

    public ProductImageRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(ProductImage productImage)
    {
       _db.Update(productImage);
    }
}
