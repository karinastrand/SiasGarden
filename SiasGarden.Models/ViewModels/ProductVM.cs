

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiasGarden.Models.ViewModels;

public class ProductVM
{
    public Product Product { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> SubCategoryList { get; set;}
    
}
