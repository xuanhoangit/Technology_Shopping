using Shop.Models;

namespace Shop.ModelProduct
{
    public class DisplayHomeCustomer
    {
        public List<Category> Categories{get;set;}
        public List<Product>? NewArrival{get;set;}
        public List<Product>? BestSeller{get;set;}
        public List<Product>? FifteenProduct{get;set;}
    }
}