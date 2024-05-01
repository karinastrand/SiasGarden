using System.ComponentModel.DataAnnotations;

namespace SiasGarden.Models;

public class SubCategory
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }

    public List<Product> Products { get; set; }
}
