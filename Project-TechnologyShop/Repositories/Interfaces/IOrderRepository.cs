using Shop.ModelProduct;
using Shop.Models;

namespace Shop.Repositories.Interfaces;
public interface IOrderRepository
{
    DisplayCheckout Checkout(List<int> order);
    bool ConfirmCheckOut(List<int> itemCartId,int addressId,Address address);
    List<DisplayOrder> DisplayOrder();
    int GetOrderCount();
    List<DisplayOrder> GetOrder(int status);
    bool ChangeStatusOrder(int orderId,int status);
    bool ConfirmOrderSuccess(int orderId);
}