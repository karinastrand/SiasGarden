

using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;
using SiasGarden.Models;

namespace SiasGarden.DataAccess.Repository;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(Product product)
    {
       var productFromDb=_db.Products.FirstOrDefault(u=>u.Id== product.Id); 
        if(productFromDb != null) 
        {
            productFromDb.Name = product.Name;
            productFromDb.Description = product.Description;
            productFromDb.Price = product.Price;
            productFromDb.ZoneFrom= product.ZoneFrom;
            productFromDb.ZoneTo= product.ZoneTo;
            productFromDb.BulkDiscount= product.BulkDiscount;
            productFromDb.Number= product.Number;
            productFromDb.LatinName= product.LatinName;
            productFromDb.CategoryId= product.CategoryId;
            if(product.StartImageUrl!=null)
            {
                productFromDb.StartImageUrl= product.StartImageUrl;
            }
        }

    }
}
