using Drink_Wholesale.Models;
using Drink_Wholesale.Services;
using Drink_Wholesale.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        // GET: Orders
        public IActionResult Index()
        {
            return View( _service.GetOrders());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            var order = _service.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create( OrderViewModel orderView)
        {

            if (ModelState.IsValid)
            {
                Order order = new() { Email = orderView.Email, PhoneNumber = orderView.PhoneNumber, Name = orderView.Name, Address = orderView.Address};

                order.Products = _service.GetCartItems(HttpContext.Session);
                _service.AddOrder(order);
                return RedirectToAction(nameof(Index));
            }
            return View(orderView);
        }
    }
}
