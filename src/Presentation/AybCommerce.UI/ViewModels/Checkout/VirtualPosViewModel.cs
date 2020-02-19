using System.ComponentModel.DataAnnotations;

namespace AybCommerce.UI.ViewModels.Checkout
{
    public class VirtualPosViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string PaymentNote { get; set; }

        [Required]
        public bool AcceptConditions { get; set; }
    }
}
