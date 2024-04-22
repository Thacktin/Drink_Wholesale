namespace Drink_Wholesale.ViewModels
{
    public class CartViewModel
    {
        public ProductViewModel ViewModel { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
