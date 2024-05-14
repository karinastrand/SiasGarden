using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiasGarden.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [Display(Name="Namn")]
    public string Name { get; set; }
    [Display(Name = "Latinskt namn")]
    public string? LatinName { get; set; }
    [Required]
    [Display(Name = "Beskrivning")]
    public string Description { get; set; }
    [Display(Name = "Pris")]
    [Range(0,5000)]
    public double Price { get; set; }
    [Display(Name = "Ljusbehov")]
    public string? Light {  get; set; }
   
    [Display(Name = "Höjd")]
    public int Height { get; set; }
    

    [Display(Name= "Växtzon max")]
    [Range(0,9)]
    public int ZoneTo { get; set; }
   
    public int SubCategoryId { get; set; }

    [ForeignKey("SubCategoryId")]
    [ValidateNever]
    public SubCategory SubCategory { get; set; }
    [ValidateNever]
    public List<ProductImage> ProductImages { get; set; }
   
    
}
