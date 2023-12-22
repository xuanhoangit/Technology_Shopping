using Shop.Models;

namespace Shop.ModelAdmin;
public class ProductAdd
{
    public Product Product { get; set; }
    public ProductDecription? ProductDecription { get; set; }
    public ProductMedia ProductMedia { get; set; }
    public List<Category>? ListCategory { get; set; }
    public List<Brand>? ListBrand { get; set; }
}