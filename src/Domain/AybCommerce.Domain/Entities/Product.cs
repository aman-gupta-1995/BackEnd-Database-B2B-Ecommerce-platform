using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;

namespace AybCommerce.Domain.Entities
{
    public class Product : AuditableEntity
    {
        [StringLength(50)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Barcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ErpCode { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Price { get; set; }

        public EntityStatus Status { get; set; }

        public int CategoryId { get; set; }

        #region [ Navigation Properties ]

        public virtual ICollection<ProductInfo> ProductInfos { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        #endregion
    }
}
