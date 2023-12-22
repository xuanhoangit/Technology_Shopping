using Shop.ModelProduct;
using Shop.Models;

namespace Shop.Repositories.Interfaces;
public interface ICartRepository
{
    Task<List<Cart>> GetCart();
    bool ChangeItemQuantity(int id,int n);
    bool AddToCart(int id);
    bool RemoveItem(int id);
}