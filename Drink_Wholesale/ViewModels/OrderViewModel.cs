using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Web.ViewModels
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Név kötelező")]
        public String Name { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Telefonszám kötelező")]
        public String PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Az email cím kötelező")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; } = null!;

        [Required(ErrorMessage = "Cím kötelező")]
        public String Address { get; set; } = null!;
    }
}
