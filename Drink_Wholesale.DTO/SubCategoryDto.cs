using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Wholesale.DTO
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public virtual List<ProductDto> Products { get; set; } = null!;
    }
}
