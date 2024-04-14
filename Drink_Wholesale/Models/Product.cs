using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drink_Wholesale.Models
{
    public enum Packaging
    {
        Single = 0,
        ShrinkWrap = 1,
        Tray = 2,
        Crate = 4
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Producer { get; set; }
        public int ArtNo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        [Required]
        public virtual SubCategory SubCategory { get; set; }
        [Column(TypeName ="money")]
        public decimal NetPrice { get; set; }
        public int Inventory { get; set; }
        [UIHint("PackagingDisplay")]
        public Packaging Packaging { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
