using System.ComponentModel;
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
        [DisplayName("Gyártó")]
        public string Producer { get; set; } = null!;
        [DisplayName("Cikkszám")]
        public int ArtNo { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Leírás")]
        public String Description { get; set; } =null!;
        public int SubCategoryId { get; set; }
        [Required]
        public virtual SubCategory SubCategory { get; set; } = null!;
        [Column(TypeName ="money")]
        [DisplayName("Nettó ár")]
        public decimal NetPrice { get; set; }
        [DisplayName("Raktárkészlet")]
        public int Inventory { get; set; }
        [UIHint("PackagingDisplay")]
        [DisplayName("Elérhető kiszerelés")]
        public Packaging Packaging { get; set; }

        public virtual List<Order> Orders { get; set; } = null!;
    }
}
