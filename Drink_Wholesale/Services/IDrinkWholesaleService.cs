using Drink_Wholesale.Models;
using Drink_Wholesale.ViewModels;

namespace Drink_Wholesale.Services;

public interface IDrinkWholesaleService
{
    void AddCategory(string name);
    List<Category> GetCategories(String? name = null);
    Category GetCategoryById(int id);
    void ChangeCategoryName(int id, string name);
    void DeleteCategory(int id);

    void AddSubcategory(SubCategory sub);
    List<SubCategory> GetSubCategoriesByCategoryId(int id);
    SubCategory GetSubCategoryById(int id);
    List<SubCategory> GetSubCategories(String? name = null);
    void ChangeSubCategoryName(string name, int id);
    void DeleteSubCategory(int id);

    void AddProduct(Product product);
    List<Product> GetProductsBySubCategoryId(int id);
    List<Product> GetAllProducts();
    Product GetProductById(int id);
    void UpdateProduct(Product product, int id);
    void DeleteProduct(int id);
    ProductViewModel NewProductViewModel(int id);
}