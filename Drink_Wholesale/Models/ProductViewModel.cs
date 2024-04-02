using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Producer { get; set; }
        public int ArtNo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public decimal NetPrice { get; set; }
        public int Inventory { get; set; }
        public decimal GrossPrice { get; set; }
        [UIHint("PackagingDisplay")]
        public Packaging Packaging { get; set; }

        public ProductViewModel(Product product)
        {
            this.Id = product.Id;
            this.Producer = product.Producer;
            this.Description = product.Description;
            this.NetPrice = product.NetPrice;
            this.Inventory = product.Inventory;
            this.ArtNo = product.ArtNo;
            this.Packaging = product.Packaging;
            this.GrossPrice = product.NetPrice * 1.27m;
        }
    }
}
