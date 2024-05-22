using Drink_Wholesale.Persistence.Services;
using Drink_Wholesale.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IDrinkWholesaleService _service;

        public CategoriesController(IDrinkWholesaleService service)
        {
            _service = service;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_service.GetCategories());
        }

        // GET: Categories/Details/5
        public IActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var category = _service.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
