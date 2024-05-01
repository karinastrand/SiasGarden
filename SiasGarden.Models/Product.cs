using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiasGarden.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string? LatinName { get; set; }
    [Required]
    public string Description { get; set; }
    public double Price { get; set; }
    public double BulkPrice { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
    public List<SubCategory> SubCategories { get; set; }
   
    
}
