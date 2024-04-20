using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Services;

public interface IOrderService
{
    List<ProductViewModel> Items { get; }
    void AddItem(ProductViewModel product);
    List<ProductViewModel> GetItems();
}