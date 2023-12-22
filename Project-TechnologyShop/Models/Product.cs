using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;
public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Tên sản phẩm")]
    public string? Name { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Giá bán gốc")]
    public float Price { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Giá giảm")]
    public float Sale { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Số lượng kho")]
    public int Quantity  { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Danh mục")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Thương hiệu")]
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public int Status { get; set; }=1;
    [NotMapped]
    public string? CategoryName { get; set; }
    [NotMapped]
    public string? BrandName { get; set; }
    [NotMapped]
    public string? Image { get; set; }
    [NotMapped]
    public List<ProductMedia>? ProductMedias { get; set; }
    [NotMapped]
    public List<ProductDecription>? ProductDecriptions { get; set; }
    [NotMapped]
    [Required(ErrorMessage ="{0} là bắt buộc")]
    [Display(Name ="Hình ảnh cho sản phẩm")]
    public List<IFormFile>? ListFiles {get;set;}
}