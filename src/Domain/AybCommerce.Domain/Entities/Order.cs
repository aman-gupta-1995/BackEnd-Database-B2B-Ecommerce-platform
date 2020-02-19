using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public sealed class Order : AuditableEntity
    {
        public Order() => OrderItems = new List<OrderItem>();

        public string UserId { get; set; }

        public int AddressId { get; set; }

        [Display(Name = "İsim")]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [StringLength(75)]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(75)")]
        public string Email { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PhoneNumber { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string OrderNote { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        public bool IsSuccess { get; set; }

        [StringLength(255)]
        public string BackRef { get; set; }

        public string PayuForm { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool? FreePayment { get; set; }

        #region [ Navigation Properties ]

        public ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Payment> Payment { get; set; }

        #endregion
    }
}
