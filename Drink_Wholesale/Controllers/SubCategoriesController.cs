using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Drink_Wholesale.Services;
using Drink_Wholesale.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Drink_Wholesale.Web.Controllers
{
    public enum SortOrder { PRODUCER_DESC, PRODUCER_ASC, PRICE_DESC, PRICE_ASC }
    public class SubCategoriesController : Controller
    {
        private readonly IDrinkWholesaleService _service;

        public SubCategoriesController(IDrinkWholesaleService service)
        {
            _service = service;
        }

        // GET: SubCategories
        public IActionResult Index()
        {
            var drinkWholesaleDbContext = _service.GetSubCategories();
            return View(drinkWholesaleDbContext);
        }

        // GET: SubCategories/Details/5
        public IActionResult Details(int id, int? page, SortOrder sortOrder = SortOrder.PRODUCER_ASC )
        {
            try
            {
                ViewData["ProducerSortParam"] = sortOrder == SortOrder.PRODUCER_DESC ? SortOrder.PRODUCER_ASC : SortOrder.PRODUCER_DESC;
                ViewData["PriceSortParam"] = sortOrder == SortOrder.PRICE_DESC ? SortOrder.PRICE_ASC : SortOrder.PRICE_DESC;
                SubCategory subCategory = _service.GetSubCategoryById(id);

                if (subCategory == null)
                {
                    return NotFound();
                }

                ViewData["Name"] = subCategory.Name;
                List<ProductViewModel> products = new();
                switch (sortOrder)
                {
                    case SortOrder.PRODUCER_ASC:
                       //products =  subCategory.Products.OrderByDescending(i => i.Producer).ToList();
                       products =  subCategory.Products.Select(p=> new ProductViewModel(){Product = p}).OrderBy(p=> p.Product!.Producer).ToList();
                        break;
                    case SortOrder.PRODUCER_DESC:
                        products = subCategory.Products.Select(p => new ProductViewModel() { Product = p }).OrderByDescending(p => p.Product!.Producer).ToList();
                        break;
                    case SortOrder.PRICE_ASC:
                        products = subCategory.Products.Select(p => new ProductViewModel() { Product = p }).OrderBy(p => p.Product!.NetPrice).ToList();
                        break;
                    case SortOrder.PRICE_DESC:
                        products = subCategory.Products.Select(p => new ProductViewModel() { Product = p }).OrderByDescending(p => p.Product!.NetPrice).ToList();
                        break;
                }

                int pageSize = 2;
                int pageNumber = (page ?? 1);
                return View(products.ToPagedList(pageNumber,pageSize));
            }
            catch (Exception)
            {

                return NotFound();
            }



        }
    }
}
