using AybCommerce.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class Address : AuditableEntity
    {
        public string UserId { get; set; }

        [Display(Name = "Adres satırı 1")]
        [StringLength(300)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Adres satırı 2")]
        [StringLength(300)]
        public string AddressLine2 { get; set; }

        [Display(Name = "Posta Code")]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        [Required]
        public string State { get; set; }

        [StringLength(50)]
        [Required]
        public string Country { get; set; } = "Türkiye";

        #region [ Navigation Propertis ]

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
