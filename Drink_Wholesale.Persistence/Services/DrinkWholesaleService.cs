using Drink_Wholesale.Models;
using Microsoft.EntityFrameworkCore;

namespace Drink_Wholesale.Persistence.Services
{
    public class DrinkWholesaleService : IDrinkWholesaleService
    {
        DrinkWholesaleDbContext _context;

        public DrinkWholesaleService(DrinkWholesaleDbContext context)
        {
            _context = context;
        }


        #region Category
        public void AddCategory(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Categories.Add(new Category { Name = name });
                _context.SaveChanges();
            }
        }

        public List<Category> GetCategories(String? name = null)
        {
            return _context.Categories
                .Where(c => c.Name.Contains(name ?? ""))
                .Include(c=> c.SubCategories)
                .OrderBy(c => c.Name)
                .ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Single(c => c.Id == id);
        }

        public void ChangeCategoryName(int id, string name)
        {
            var category = _context.Categories.Single(c => c.Id == id);
            category.Name = name;
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Single(c => c.Id == id);
            if (category == null)
            {
                return;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        #endregion

        #region SubCategory

        public void AddSubcategory(SubCategory sub)
        {
            if (sub == null)
            {
                return;
            }
            _context.SubCategories.Add(sub);
            _context.SaveChanges();

        }

        public List<SubCategory> GetSubCategoriesByCategoryId(int id)
        {
            return _context.SubCategories
                .Where(s => s.CategoryId == id)
                .ToList();
        }

        public SubCategory GetSubCategoryById(int id)
        {
            return _context.SubCategories.Single(s => s.Id == id);
        }

        public List<SubCategory> GetSubCategories(String? name = null)
        {
            return _context.SubCategories
                .Where(s => s.Name.Contains(name ?? ""))
                .Include(s=> s.Category)
                .ToList();
        }

        public void ChangeSubCategoryName(string name, int id)
        {
            var subCategory = _context.SubCategories.Single(s => s.Id == id);

            subCategory.Name = name;
            _context.SaveChanges();
        }

        public void DeleteSubCategory(int id)
        {
            var subCategory = _context.SubCategories.Single(s => s.Id == id);

            if (subCategory == null)
            {
                return;
            }

            _context.SubCategories.Remove(subCategory);
            _context.SaveChanges();
        }
        #endregion

        #region Products




        public void AddProduct(Product product)
        {
            if (product == null)
            {
                return;
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<Product> GetProductsBySubCategoryId(int id)
        {
            return _context.Products
                .Where(p => p.SubCategory.Id == id)
                .ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Single(p => p.Id == id);
        }

        public void UpdateProduct(Product product, int id)
        {
            var oldProduct = _context.Products.Single(p => p.Id == id);

            if (oldProduct == null)
            {
                return;
            }

            oldProduct.SubCategory = product.SubCategory;
            oldProduct.Description = product.Description;
            oldProduct.ArtNo = product.ArtNo;
            oldProduct.Inventory = product.Inventory;
            oldProduct.NetPrice = product.NetPrice;
            oldProduct.Producer = product.Producer;
            oldProduct.Packaging = product.Packaging;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Single(p => p.Id == id);
            if (product == null)
            {
                return;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        //public ProductViewModel NewProductViewModel(int id)
        //{
        //    var product = _context.Products.Single(p => p.Id == id);
        //    return new ProductViewModel
        //    {
        //        Product = product,
        //        //GrossPrice = product.NetPrice * 1.27m
        //    };
        //}
        #endregion

    }
}
