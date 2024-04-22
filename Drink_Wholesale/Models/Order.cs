using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező")]
        public string Name { get; set; } = null!;
        [Required]
        public String Address { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "A telefonszám kötelező")]
        [MaxLength(12)]
        public String PhoneNumber { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "A cím megadása kötelező")]
        public String Email { get; set; } = null!;

        public virtual List<CartItem> Products { get; set; } = null!;
        public bool IsFulfilled  { get; set; }
    }
}
