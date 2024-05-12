

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiasGarden.Models.ViewModels;

public class UserVM
{
    public ApplicationUser ApplicationUser { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> RoleList { get; set; }
}
