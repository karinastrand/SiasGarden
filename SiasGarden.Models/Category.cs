using System.ComponentModel.DataAnnotations;

namespace SiasGarden.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Namn")]
    [MaxLength(50,ErrorMessage="Maxlängd är 50 tecken")]
    public string Name { get; set; }
    [MaxLength(100, ErrorMessage = "Maxlängd är 100 tecken")]
    [Display(Name = "Beskrivning")]
    public string? Description { get; set; }
    [Display(Name = "Sorteringsordning")]
    [Range(1,1000,ErrorMessage ="Sorteringsordning måste vara mellan 1 och 1000")]
    public int DisplayOrder { get; set; }

}
