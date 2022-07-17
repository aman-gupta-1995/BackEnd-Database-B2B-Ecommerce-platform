using System.ComponentModel.DataAnnotations;

namespace AybCommerce.UI.ViewModels.Home
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required] 
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
