using Drink_Wholesale.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _service;

        public CartController(IOrderService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var list = _service.GetCartViewModels(HttpContext.Session);

            decimal totalNetPrice = list.Aggregate(0m, (x, l) => x + l.TotalQuantity * l.ViewModel.Product!.NetPrice);
            ViewData["totalNetPrice"] = Math.Round(totalNetPrice,2).ToString();
            decimal totalGrossPrice = list.Aggregate(0m, (x, l) => x + l.TotalQuantity * l.ViewModel.GrossPrice);
            ViewData["TotalGrossPrice"] = Math.Round(totalGrossPrice, 2).ToString();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            if (id == -1)
            {
                _service.DiscardCart(HttpContext.Session);
                var list = _service.GetCartViewModels(HttpContext.Session);
                return View("Index", list);
            }
            else
            {
                var cartItem = _service.GetItemFromCart(id, HttpContext.Session);
                _service.RemoveItem(id, HttpContext.Session);
                var list = _service.GetCartViewModels(HttpContext.Session);
                return View("Index", list);
            }

        }

    }
}
