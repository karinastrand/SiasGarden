

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
            productFromDb.LatinName = product.LatinName;
            productFromDb.Description = product.Description;
            productFromDb.Price = product.Price;
            productFromDb.Light = product.Light;
            productFromDb.Height = product.Height;
            productFromDb.ZoneTo= product.ZoneTo;
            productFromDb.SubCategoryId= product.SubCategoryId;
            productFromDb.ProductImages= product.ProductImages;
        }

    }
}
