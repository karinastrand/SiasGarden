﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    [Display(Name = "Mängdrabatt, %")]
    [Range(0,100)]
    public int BulkDiscount { get; set; }
    [Display(Name = "Antal")]

    [Range(0, 100)]
    public int Number { get; set; }
    [Display(Name= "Växtzon max")]
    [Range(0,8)]
    public int ZoneTo { get; set; }
    [Display(Name = "Växtzon min")]
    [Range(0, 8)]
    public int ZoneFrom { get; set; }
    public string? StartImageUrl { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
    [ValidateNever]
    public List<SubCategory> SubCategories { get; set; }
    [ValidateNever]
    public List<ProductImage> ProductImages { get; set; }
   
    
}
