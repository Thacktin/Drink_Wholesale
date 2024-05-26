using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Helpers;
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

        public SubCategory? AddSubcategory(SubCategory sub)
        {
            try
            {
                _context.SubCategories.Add(sub);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                return null;
            }

            return sub;




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
                .Include(s => s.Category)
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




        public Product AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                return null;
            }

            return product;
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
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
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

        public Order? AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception e)
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
            throw new NotImplementedException();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public bool ChangeOrderState(Order order)
        {
            List<Product> productToUpdate = new();
            var products = order.Products;
            if (order.IsFulfilled)
            {
                foreach (var product in products)
                {
                    Product p = product.Product;
                    if (p != null)
                    {
                        p.Inventory += product.Quantity * EnumHelpers.PackagintToInt(product.Packaging);
                        productToUpdate.Add(p);
                    }

                }
                order.IsFulfilled = true;
            }
            else
            {
                foreach (var product in products)
                {
                    Product p = product.Product;
                    if (p != null)
                    {
                        p.Inventory -= product.Quantity * EnumHelpers.PackagintToInt(product.Packaging);
                        productToUpdate.Add(p);
                    }

                }
                order.IsFulfilled = false;
            }

            try
            {
                productToUpdate.Select(p => _context.Products.Update(p));
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }



            return true;
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
