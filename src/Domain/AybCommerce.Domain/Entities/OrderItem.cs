using AybCommerce.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public int OrderId { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ProductCode { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ProductName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Currency { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        #endregion

    }
}