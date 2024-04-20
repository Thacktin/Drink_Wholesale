using Drink_Wholesale.Models;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Services
{
    public struct CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Packaging Packaging { get; set; }
    }
    public class OrderService
    {
        private List<CartItem> _cart = [];
        public List<ProductViewModel> Items => GetItems();
        private readonly IDrinkWholesaleService _service;
        private DrinkWholesaleDbContext _context;

        public OrderService(DrinkWholesaleDbContext context, IDrinkWholesaleService service)
        {
            _context = context;
            _service = service;
        }
        public void AddItem(ProductViewModel product)
        {
            if (product.Product == null) throw new ArgumentNullException();
            CartItem item = new CartItem()
                { ProductId = product.Product.Id, Packaging = product.SelectedPackaging, Quantity = product.Quantity };
            _cart.Add(item);
        }

        public List<ProductViewModel> GetItems()
        {
            var items = _cart.Select(i => new ProductViewModel()
            {
                Product = _service.GetProductById(i.ProductId),
                Quantity = i.Quantity,
                SelectedPackaging = i.Packaging
            }).ToList();
            return items;
        }
    }
}
