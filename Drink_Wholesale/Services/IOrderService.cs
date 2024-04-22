using Drink_Wholesale.Models;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Services;

public interface IOrderService
{
    //List<ProductViewModel> Items { get; }
    void AddItem(ProductViewModel product, ISession session);
    List<CartViewModel> GetCartViewModels(ISession session);
    List<CartItem> GetCartItems(ISession session);

    void AddOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(int id);
    Order GetOrderById(int id);
    List<Order> GetOrders();
    CartViewModel GetItemFromCart(int id);
}