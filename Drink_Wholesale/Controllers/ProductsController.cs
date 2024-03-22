using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drink_Wholesale.Models;
using Drink_Wholesale.Servicies;

namespace Drink_Wholesale.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDrinkWholesaleService _service;

        public ProductsController(IDrinkWholesaleService service)
        {
            _service = service;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var drinkWholesaleDbContext = _service.GetAllProducts();
            return View(drinkWholesaleDbContext);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
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
