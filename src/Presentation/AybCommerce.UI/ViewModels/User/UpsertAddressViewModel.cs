using AybCommerce.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AybCommerce.UI.ViewModels.User
{
    public class UpsertAddressViewModel
    {
        public int AddressId { get; set; }

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

        public Address BuildAddress(string userId)
        {
            return new Address
            {
                Id = AddressId,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                ZipCode = ZipCode,
                City = City,
                State = State,
                UserId = userId
            };
        }
    }
}
