using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drink_Wholesale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drink_Wholesale.Services;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Controllers
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
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = _service.NewProductViewModel(id);
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

            bool good = (productViewModel.Product.Packaging & productViewModel.SelectedPackaging) == 0;
            if ((productViewModel.Product.Packaging & productViewModel.SelectedPackaging) == 0 ^ productViewModel.SelectedPackaging == Packaging.Single) 
            {
                ModelState.AddModelError("SelectedPackaging", "A termék a megadott kiszerelésben nem elérhető");
            }

            if (productViewModel.Quantity * Helpers.EnumHelpers.PackagintToInt(productViewModel.SelectedPackaging)> productViewModel.Product.Inventory)
            {
                ModelState.AddModelError("Quantity", "A termékből nincs elég raktáron");
            }
            if (!ModelState.IsValid)
            {
                ViewData["OrderResult"] = "Hiba";
                return View("Details", productViewModel);
            }
            
            var cart = _orderService.GetCartViewModels(HttpContext.Session);
            //cart ??= new List<(int,Packaging)>();

            //if (cart.Contains((productViewModel.Product.ArtNo, productViewModel.Product.Packaging)))
            //{
            //    ModelState.AddModelError("packagingExists", "A termék már szerepel a kosárban máás kiszerelésben");
            //}

            //int selectedSize = 12;
            //int quantity = 2;

            //if (productViewModel.Inventory > selectedSize * quantity)
            //{
            //    cart.Add((id, productViewModel.Packaging));
            //}
            //cart.Add((id, productViewModel.SelectedPackaging));
            _orderService.AddItem(productViewModel, HttpContext.Session);
            //SessionExtensions.Set<List<(int,Packaging)>>(HttpContext.Session, "cart",cart);
            cart = _orderService.GetCartViewModels(HttpContext.Session);
            ViewData["OrderResult"] = "A termék hozzáadva a kosárhoz";
            
            return View(productViewModel);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = _service.GetProductById(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //ProductViewModel productViewModel = new ProductViewModel(product);
            //return View(productViewModel);
        }


        // GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["SubCategoryId"] = new SelectList(_service.SubCategories, "Id", "Id");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Producer,ArtNo,Description,SubCategoryId,NetPrice,Inventory,Packaging")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _service.Add(product);
        //        await _service.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SubCategoryId"] = new SelectList(_service.SubCategories, "Id", "Id", product.SubCategoryId);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _service.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["SubCategoryId"] = new SelectList(_service.SubCategories, "Id", "Id", product.SubCategoryId);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Producer,ArtNo,Description,SubCategoryId,NetPrice,Inventory,Packaging")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _service.Update(product);
        //            await _service.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SubCategoryId"] = new SelectList(_service.SubCategories, "Id", "Id", product.SubCategoryId);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _service.Products
        //        .Include(p => p.SubCategory)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _service.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _service.Products.Remove(product);
        //    }

        //    await _service.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //    return _service.Products.Any(e => e.Id == id);
        //}
    }
}
