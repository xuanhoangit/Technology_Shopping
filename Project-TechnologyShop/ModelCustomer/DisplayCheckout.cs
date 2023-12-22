using Shop.Models;

namespace Shop.ModelProduct;
public class DisplayCheckout
{
    public List<Cart>? ListCart { get; set; }
    public List<Address>? ListAddress { get; set; }
    public Address? Address { get; set; }
}