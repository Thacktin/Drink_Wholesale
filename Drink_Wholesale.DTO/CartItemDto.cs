using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Wholesale.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public virtual ProductDto? Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Packaging Packaging { get; set; }
    }
}
