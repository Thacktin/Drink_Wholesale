using Drink_Wholesale.Models;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Services
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

<<<<<<< Updated upstream:Drink_Wholesale/Services/DrinkWholesaleService.cs
        public void AddSubcategory(SubCategory sub)
=======
        public SubCategory? AddSubcategory(SubCategory sub)
>>>>>>> Stashed changes:Drink_Wholesale.Persistence/Services/DrinkWholesaleService.cs
        {
            if (sub == null)
            {
                return;
            }
<<<<<<< Updated upstream:Drink_Wholesale/Services/DrinkWholesaleService.cs
            _context.SubCategories.Add(sub);
            _context.SaveChanges();
=======
            catch (Exception)
            {

                return null;
            }

            return sub;



>>>>>>> Stashed changes:Drink_Wholesale.Persistence/Services/DrinkWholesaleService.cs

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
<<<<<<< Updated upstream:Drink_Wholesale/Services/DrinkWholesaleService.cs
=======
                .Include(s => s.Category)
>>>>>>> Stashed changes:Drink_Wholesale.Persistence/Services/DrinkWholesaleService.cs
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

        public bool UpdateProduct(Product product)
        {
            //var oldProduct = _context.Products.Single(p => p.Id == id);

            //if (oldProduct == null)
            //{
            //    return;
            //}

            //oldProduct.SubCategory = product.SubCategory;
            //oldProduct.Description = product.Description;
            //oldProduct.ArtNo = product.ArtNo;
            //oldProduct.Inventory = product.Inventory;
            //oldProduct.NetPrice = product.NetPrice;
            //oldProduct.Producer = product.Producer;
            //oldProduct.Packaging = product.Packaging;
            try
            {
                _context.Update(product);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
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
        #endregion

        #region Order



        public Order? AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return order;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Single(o => o.Id == id);
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public ProductViewModel NewProductViewModel(int id)
        {
            var product = _context.Products.Single(p => p.Id == id);
            return new ProductViewModel
            {
                Product = product,
                //GrossPrice = product.NetPrice * 1.27m
            };
        }
        #endregion

    }
}
