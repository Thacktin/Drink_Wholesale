using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
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
    }
}
