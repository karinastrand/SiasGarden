using System.ComponentModel.DataAnnotations;

namespace SiasGarden.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Namn")]
    [MaxLength(50,ErrorMessage="Maxlängd är 50 tecken")]
    public string Name { get; set; }
  
   
    
}
