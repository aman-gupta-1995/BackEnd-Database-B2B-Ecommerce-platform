using AybCommerce.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class CartItem : AuditableEntity
    {
        [StringLength(255)]
        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(255)]
        public string CartId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ProductName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Currency { get; set; }

        #region [ Navigation Properties ]


        #endregion
    }
}
