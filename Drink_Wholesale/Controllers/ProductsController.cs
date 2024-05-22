using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Drink_Wholesale.Services;
using Drink_Wholesale.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDrinkWholesaleService _service;
        private readonly IOrderService _orderService;

        public ProductsController(IDrinkWholesaleService service, IOrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        // GET: Products
        public IActionResult Index()
        {
            var drinkWholesaleDbContext = _service.GetAllProducts();
            return View(drinkWholesaleDbContext);
        }

        // GET: Products/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {

            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel(){Product = product};
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int id, ProductViewModel productViewModel, Int32 productId)
        {
            productViewModel.Product = _service.GetProductById(productId);

            if (productViewModel.Quantity < 1)
            {
                ModelState.AddModelError("Quantity", "Min 1");
            }

            //bool good = (productViewModel.Product.Packaging & productViewModel.SelectedPackaging) == 0;
            if ((productViewModel.Product.Packaging & productViewModel.SelectedPackaging) == 0 ^ productViewModel.SelectedPackaging == Packaging.Single) 
            {
                ModelState.AddModelError("SelectedPackaging", "A termék a megadott kiszerelésben nem elérhető");
            }

            if (productViewModel.Quantity * Helpers.EnumHelpers.PackagintToInt(productViewModel.SelectedPackaging)> productViewModel.Product.Inventory)
            {
                ModelState.AddModelError("Quantity", "A termékből nincs elég raktáron");
            }

            if (_orderService.CheckIfAlreadyInCart(productViewModel, HttpContext.Session))
            {
                ModelState.AddModelError("SelectedPackaging", "A termék már a kosárban van");
            }


            if (!ModelState.IsValid)
            {
                ViewData["OrderResult"] = "Hiba";
                return View("Details", productViewModel);
            }
            
            var cart = _orderService.GetCartViewModels(HttpContext.Session);

            _orderService.AddItem(productViewModel, HttpContext.Session);
            cart = _orderService.GetCartViewModels(HttpContext.Session);
            ViewData["OrderResult"] = "A termék hozzáadva a kosárhoz";
            
            return View(productViewModel);
        }


        
    }
}
