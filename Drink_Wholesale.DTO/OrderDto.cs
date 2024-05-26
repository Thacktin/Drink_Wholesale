using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Wholesale.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public String Address { get; set; } = null!;
        public String PhoneNumber { get; set; } = null!;
        public String Email { get; set; } = null!;

        public virtual List<CartItemDto> Products { get; set; } = null!;
        public bool IsFulfilled { get; set; }
    }
}
