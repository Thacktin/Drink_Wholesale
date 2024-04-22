using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.ViewModels
{
    public class OrderViewModel
    {
        public String Name { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Telefonszám kötelező")]
        public String PhoneNumber { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public String Email { get; set; } = null!;

        public String Address { get; set; }
    }
}
