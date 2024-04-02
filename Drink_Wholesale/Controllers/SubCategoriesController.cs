using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drink_Wholesale.Models;
using Drink_Wholesale.Services;
using X.PagedList;

namespace Drink_Wholesale.Controllers
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
        public async Task<IActionResult> Index()
        {
            var drinkWholesaleDbContext = _service.GetSubCategories();
            return View(drinkWholesaleDbContext);
        }

        // GET: SubCategories/Details/5
        public async Task<IActionResult> Details(int id,int? page, SortOrder sortOrder = SortOrder.PRODUCER_ASC )
        {
            try
            {
                ViewData["ProducerSortParam"] = sortOrder == SortOrder.PRODUCER_DESC ? SortOrder.PRODUCER_ASC : SortOrder.PRODUCER_DESC;
                ViewData["PriceSortParam"] = sortOrder == SortOrder.PRICE_DESC ? SortOrder.PRICE_ASC : SortOrder.PRICE_DESC;
                SubCategory subCategory = _service.GetSubCategoryById(id);

                switch (sortOrder)
                {
                    case SortOrder.PRODUCER_ASC:
                        subCategory.Products.OrderByDescending(i => i.Producer).ToList();
                        break;
                    case SortOrder.PRODUCER_DESC:
                        subCategory.Products.OrderBy(i => i.Producer).ToList();
                        break;
                    case SortOrder.PRICE_ASC:
                        subCategory.Products.OrderByDescending(i => i.NetPrice).ToList();
                        break;
                    case SortOrder.PRICE_DESC:
                        subCategory.Products.OrderBy(i => i.NetPrice).ToList();
                        break;
                }

                int pageSize = 2;
                int pageNumber = page ?? 1;
                //return View(subCategory);
                //var OnepageOfProducts = subCategory.Products.ToPagedList(pageNumber, pageSize);
                //ViewBag.OnePageOfProducts = OnepageOfProducts;
                //SubCategoryViewModel subCategoryViewModel = new SubCategoryViewModel(subCategory, );
                return View(subCategory.Products.ToPagedList(pageNumber,pageSize));
            }
            catch (Exception e)
            {

                return NotFound();
            }
            //var subCategory =  _service.GetSubCategoryById(id);
            //if (subCategory == null)
            //{
            //    return NotFound();
            //}


        }

        // GET: SubCategories/Create
    //    public IActionResult Create()
    //    {
    //        ViewData["CategoryId"] = new SelectList(_service.Categories, "Id", "Id");
    //        return View();
    //    }

    //    // POST: SubCategories/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,Name,CategoryId")] SubCategory subCategory)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _service.Add(subCategory);
    //            await _service.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewData["CategoryId"] = new SelectList(_service.Categories, "Id", "Id", subCategory.CategoryId);
    //        return View(subCategory);
    //    }

    //    // GET: SubCategories/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var subCategory = await _service.SubCategories.FindAsync(id);
    //        if (subCategory == null)
    //        {
    //            return NotFound();
    //        }
    //        ViewData["CategoryId"] = new SelectList(_service.Categories, "Id", "Id", subCategory.CategoryId);
    //        return View(subCategory);
    //    }

    //    // POST: SubCategories/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] SubCategory subCategory)
    //    {
    //        if (id != subCategory.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _service.Update(subCategory);
    //                await _service.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!SubCategoryExists(subCategory.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewData["CategoryId"] = new SelectList(_service.Categories, "Id", "Id", subCategory.CategoryId);
    //        return View(subCategory);
    //    }

    //    // GET: SubCategories/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var subCategory = await _service.SubCategories
    //            .Include(s => s.Category)
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (subCategory == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(subCategory);
    //    }

    //    // POST: SubCategories/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var subCategory = await _service.SubCategories.FindAsync(id);
    //        if (subCategory != null)
    //        {
    //            _service.SubCategories.Remove(subCategory);
    //        }

    //        await _service.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool SubCategoryExists(int id)
    //    {
    //        return _service.SubCategories.Any(e => e.Id == id);
    //    }
    }
}
