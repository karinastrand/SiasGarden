

namespace SiasGarden.DataAccess.Repository.IRepository;

public  interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    ISubCategoryRepository SubCategory { get; }
    IProductImageRepository ProductImage { get; }
    void Save();
}
