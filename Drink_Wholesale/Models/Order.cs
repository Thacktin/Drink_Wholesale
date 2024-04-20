using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual List<Product> Products { get; set; }
        public bool IsFulfilled  { get; set; }
    }
}
