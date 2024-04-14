using System.ComponentModel.DataAnnotations;
using Drink_Wholesale.Models;

namespace Drink_Wholesale.ViewModels
{
    public class ProductViewModel
    {
        public string Producer { get; set; }
        public int ArtNo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public decimal NetPrice { get; set; }
        public int Inventory { get; set; }
        public decimal GrossPrice { get; set; }
        [UIHint("PackagingDisplay")]
        public Packaging Packaging { get; set; }
        public Packaging SelectedPackaging { get; set; }

        public ProductViewModel(Product product)
        {
            Producer = product.Producer;
            Description = product.Description;
            NetPrice = product.NetPrice;
            Inventory = product.Inventory;
            GrossPrice = product.NetPrice * 1.27m;
            Packaging = product.Packaging;
            ArtNo = product.ArtNo;
        }

        public ProductViewModel()
        {
            
        }
    }
}
