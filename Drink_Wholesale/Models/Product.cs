using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drink_Wholesale.Models
{
    [Flags]
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
        public string Producer { get; set; } = null!;
        public int ArtNo { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; } =null!;
        public int SubCategoryId { get; set; }
        [Required]
        public virtual SubCategory SubCategory { get; set; } = null!;
        [Column(TypeName ="money")]
        public decimal NetPrice { get; set; }
        public int Inventory { get; set; }
        [UIHint("PackagingDisplay")]
        public Packaging Packaging { get; set; }

        public virtual List<Order> Orders { get; set; } = null!;
    }
}
