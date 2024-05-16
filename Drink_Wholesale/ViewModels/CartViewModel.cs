namespace Drink_Wholesale.Web.ViewModels
{
    public class CartViewModel
    {
        public ProductViewModel ViewModel { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public decimal TotalPrice => this.ViewModel.GrossPrice * this.TotalQuantity;
    }
}
