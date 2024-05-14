using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiasGarden.Models;

public class SubCategory
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Namn")]    
    public string Name { get; set; }
   
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    [ValidateNever]
    public Category Category { get; set; }

  
}
