using Shop.Models;

namespace Shop.ModelProduct;
public class DisplayOrder
{
    public Order Order { get; set; }
    public Address Address { get; set; }
    public List<OrderDetail> ListOrderDetail { get; set; }
}