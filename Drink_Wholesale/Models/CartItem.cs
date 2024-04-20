using Drink_Wholesale.Services;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Models
{
    public struct CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Packaging Packaging { get; set; }
    }

    public class Cart
    {
        public List<ProductViewModel> Items => GetItems();
        private List<CartItem> _items = new List<CartItem>();
        private IDrinkWholesaleService _service;

        public void AddItem(ProductViewModel product)
        {
            if (product.Product == null) throw new ArgumentNullException();
            CartItem item = new CartItem()
                { ProductId = product.Product.Id, Packaging = product.SelectedPackaging, Quantity = product.Quantity };
            _items.Add(item);
        }

        public List<ProductViewModel> GetItems()
        {
            var items = _items.Select(i => new ProductViewModel()
            {
                Product = _service.GetProductById(i.ProductId), Quantity = i.Quantity, SelectedPackaging = i.Packaging
            }).ToList();
            return items;
        }

}
}
