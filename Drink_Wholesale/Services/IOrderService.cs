using Drink_Wholesale.Models;
using Drink_Wholesale.Web.ViewModels;

namespace Drink_Wholesale.Services;

public interface IOrderService
{
    //List<ProductViewModel> Items { get; }
    void AddItem(ProductViewModel product, ISession session);
    void RemoveItem(int id, ISession session);
    List<CartViewModel> GetCartViewModels(ISession session);
    List<CartItem> GetCartItems(ISession session);

    void AddOrder(Order order);
    Order GetOrderById(int id);
    List<Order> GetOrders();
    CartViewModel GetItemFromCart(int id, ISession session);
    bool CheckIfAlreadyInCart(ProductViewModel productViewModel, ISession session);
    void DiscardCart(ISession session);
}