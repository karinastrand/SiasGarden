

namespace SiasGarden.DataAccess.Repository.IRepository;

public  interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    ISubCategoryRepository SubCategory { get; }
    IProductImageRepository ProductImage { get; }
    IShoppingCartRepository ShoppingCart { get; }
    IApplicationUserRepository ApplicationUser { get; }
    IOrderHeaderRepository OrderHeader { get; }
    IOrderDetailRepository OrderDetail { get; }
    void Save();
}
