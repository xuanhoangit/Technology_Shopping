using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        // public string? Image { get; set; }
        // [NotMapped]
        // public IFormFile? File { get; set; }
        public int Status { get; set; }=1;
        [NotMapped]
        public List<Brand>? ListBrand { get; set; }
    }
}