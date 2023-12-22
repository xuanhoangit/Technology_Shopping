using Shop.Models;

namespace Shop.ModelProduct;
public class DisplayProductDetail
{
        public Product Product { get; set; }
        public Category Category { get; set; }
        public List<ProductMedia> ProductMedias { get; set; }
        public List<ProductDecription> ProductDecriptions { get; set; }
}