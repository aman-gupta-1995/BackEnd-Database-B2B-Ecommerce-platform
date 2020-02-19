using System.ComponentModel.DataAnnotations;

namespace AybCommerce.UI.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-Mail")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
