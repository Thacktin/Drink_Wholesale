using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Wholesale.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public String Name { get; set; } = null!;
        public virtual List<SubCategoryDto> SubCategories { get; set; } = null!;
    }
}
