namespace Drink_Wholesale.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public virtual Product? Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Packaging Packaging { get; set; }
    }
}

