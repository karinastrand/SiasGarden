﻿
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiasGarden.Models.ViewModels;

public class SubCategoryVM
{
    public SubCategory SubCategory { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
    
}
