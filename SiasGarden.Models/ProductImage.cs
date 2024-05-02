

using System.ComponentModel.DataAnnotations.Schema;

namespace SiasGarden.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public int ProdutId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}
