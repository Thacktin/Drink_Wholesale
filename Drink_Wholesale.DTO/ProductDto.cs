using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Wholesale.DTO
{
    public enum Packaging
    {
        Single = 0,
        ShrinkWrap = 1,
        Tray = 2,
        Crate = 4
    }
    public class ProductDto
    {

            public int Id { get; set; }
            public string Producer { get; set; } = null!;
            public int ArtNo { get; set; }
            public String Description { get; set; } = null!;
            public int SubCategoryId { get; set; }
            public decimal NetPrice { get; set; }
            public int Inventory { get; set; }
            public Packaging Packaging { get; set; }
    }
}
