
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ProductMedia
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Status { get; set; }=1;
    }
}