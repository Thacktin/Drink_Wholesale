using System.ComponentModel.DataAnnotations;
using Drink_Wholesale.Models;

namespace Drink_Wholesale.ViewModels
{
    public class ProductViewModel
    {

        public  Product? Product { get; set; }
        public decimal GrossPrice => Product != null ? Product.NetPrice * 1.27m: 0;
        [UIHint("PackagingDisplay")]
        public Packaging SelectedPackaging { get; set; }
        public int Quantity { get; set; }

    }
}
