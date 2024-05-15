
namespace SiasGarden.Models.ViewModels;

public class ProductVM2
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categorys { get; set; }
    public string? SelectedCategory { get; set; } = "Alla";
}
