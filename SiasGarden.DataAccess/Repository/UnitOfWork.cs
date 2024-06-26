﻿
using SiasGarden.DataAccess.Data;
using SiasGarden.DataAccess.Repository.IRepository;

namespace SiasGarden.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ISubCategoryRepository SubCategory { get; private set; }
    public IProductImageRepository ProductImage { get; private set; }
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public IApplicationUserRepository ApplicationUser { get; private set; }
    public IOrderHeaderRepository OrderHeader { get; private set; }
    public IOrderDetailRepository OrderDetail { get; private set; }

    private ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category=new CategoryRepository(_db);
        Product=new ProductRepository(_db);
        SubCategory=new SubCategoryRepository(_db);
        ProductImage=new ProductImageRepository(_db);
        ShoppingCart=new ShoppingCartRepository(_db);
        ApplicationUser=new ApplicationUserRepository(_db);
        OrderHeader=new OrderHeaderRepository(_db); 
        OrderDetail=new OrderDetailRepository(_db);
    }

    public void Save()
    {
       _db.SaveChanges();
    }
}
