using Drink_Wholesale.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drink_Wholesale.Admin.Helpers;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public virtual ProductViewModel Product { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Packaging Packaging { get; set; }
        
        public decimal TotalQuantity => this.Quantity * Helpers.EnumHelpers.PackagintToInt(this.Packaging);
    }
}
