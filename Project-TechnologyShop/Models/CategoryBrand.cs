using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class CategoryBrand
    {
        public int Id { get; set; }
        [Required]
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int Status { get; set; }=1;
        [NotMapped]
        public string? CategoryName { get; set; }
        [NotMapped]
        public string? BrandName { get; set; }
    }
}