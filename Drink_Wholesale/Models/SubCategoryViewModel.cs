namespace Drink_Wholesale.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<Product> Products { get; set; }

        public SubCategoryViewModel(SubCategory subCategory, List<Product> products)
        {
            this.Id = subCategory.Id;
            this.Name = subCategory.Name;
            this.CategoryId = subCategory.CategoryId;
            this.Products = products;
        }
    }
}
