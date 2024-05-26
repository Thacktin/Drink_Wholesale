using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        [Required] 
        public virtual Category Category { get; set; } = null!;
        public virtual List<Product> Products { get; set; } = null!;
    }
}