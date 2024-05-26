using Drink_Wholesale.Models;

namespace Drink_Wholesale.Persistence.Services;

public interface IDrinkWholesaleService
{
    void AddCategory(string name);
    List<Category> GetCategories(String? name = null);
    Category GetCategoryById(int id);
    void ChangeCategoryName(int id, string name);
    void DeleteCategory(int id);

    SubCategory? AddSubcategory(SubCategory sub);
    List<SubCategory> GetSubCategoriesByCategoryId(int id);
    SubCategory GetSubCategoryById(int id);
    List<SubCategory> GetSubCategories(String? name = null);
    void ChangeSubCategoryName(string name, int id);
    void DeleteSubCategory(int id);

    Product? AddProduct(Product product);
    List<Product> GetProductsBySubCategoryId(int id);
    List<Product> GetAllProducts();
    Product GetProductById(int id);
    bool UpdateProduct(Product product);
    void DeleteProduct(int id);
    //ProductViewModel NewProductViewModel(int id);

    Order? AddOrder(Order order);
    Order GetOrderById(int id);
    List<Order> GetAllOrders();
    bool UpdateOrder(Order order);
    void DeleteOrder(int id);
     bool ChangeOrderState(Order order);
}