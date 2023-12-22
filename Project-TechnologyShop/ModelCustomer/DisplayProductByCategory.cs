using Shop.Models;

namespace Shop.ModelProduct;
public class DisplayProductByCategory
{
    public List<Product> Product { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public int CategoryId{get;set;}
    public int BrandId{get;set;}
    public List<CategoryBrand>? ListBrand { get; set; }
    public List<Category>? ListCategory {get;set;}
}