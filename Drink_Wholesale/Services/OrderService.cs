using Drink_Wholesale.Models;
using Drink_Wholesale.ViewModels;
using Drink_Wholesale.Controllers;
using Drink_Wholesale.Helpers;

namespace Drink_Wholesale.Services
{
    //public struct CartItem
    //{
    //    public int ProductId { get; set; }
    //    public int Quantity { get; set; }
    //    public Packaging Packaging { get; set; }
    //}


    public class OrderService : IOrderService
    {
        private List<CartItem> _cart;

        //public List<ProductViewModel> Items => GetCartViewModels();
        private readonly IDrinkWholesaleService _service;
        private readonly DrinkWholesaleDbContext _context;

        public OrderService(DrinkWholesaleDbContext context, IDrinkWholesaleService service)
        {
            _context = context;
            _service = service;
            _cart = new();
        }
        #region Cart

        public void AddItem(ProductViewModel product, ISession session)
        {
            _cart = Controllers.SessionExtensions.Get<List<CartItem>>(session, "cart");
            if (product.Product == null) throw new ArgumentNullException();
            _cart ??= [];
            CartItem item = new CartItem()
            { ProductId = product.Product.Id, Packaging = product.SelectedPackaging, Quantity = product.Quantity };
            _cart.Add(item);
            Controllers.SessionExtensions.Set<List<CartItem>>(session, "cart", _cart);
        }

        public void RemoveItem(int id, ISession session)
        {
            _cart = Controllers.SessionExtensions.Get<List<CartItem>>(session, "cart");
            _cart.RemoveAll(i => i.ProductId == id);
            Controllers.SessionExtensions.Set<List<CartItem>>(session, "cart", _cart);
        }

        public CartViewModel GetItemFromCart(int id)
        {
            //return new CartViewModel() { }.Single(c => c.ProductId == id);
            throw new NotImplementedException();
        }

        private CartViewModel NewCartViewModel(CartItem cartItem)
        {
            return new CartViewModel()
            {
                ViewModel = new ProductViewModel()
                {
                    Product = _service.GetProductById(cartItem.ProductId),
                    Quantity = cartItem.Quantity,
                    SelectedPackaging = cartItem.Packaging
                },
                TotalQuantity = cartItem.Quantity * EnumHelpers.PackagintToInt(cartItem.Packaging)
            };
        }

        public List<CartViewModel> GetCartViewModels(ISession session)
        {
            _cart = Controllers.SessionExtensions.Get<List<CartItem>>(session, "cart");
            if (_cart == null)
            {
                return new();
            }

            //var items = _cart.Select(i => new CartViewModel()
            //{
            //    ViewModel = new ProductViewModel()
            //    {
            //        Product = _service.GetProductById(i.ProductId),
            //        Quantity = i.Quantity,
            //        SelectedPackaging = i.Packaging
            //    },
            //    TotalQuantity = i.Quantity * EnumHelpers.PackagintToInt(i.Packaging)


            //}).ToList();
            var items = _cart.Select(c => NewCartViewModel(c)).ToList();
            return items;
        }

        public List<CartItem> GetCartItems(ISession session)
        {
            return Controllers.SessionExtensions.Get<List<CartItem>>(session, "cart");
        }


        #endregion

        #region Orders

        public void AddOrder(Order order)
        {
            if (order == null)
            {
                return;

            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Single(o => o.Id == id);
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();

        }



        #endregion
    }
}
