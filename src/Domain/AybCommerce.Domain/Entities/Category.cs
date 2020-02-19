using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class Category : AuditableEntity
    {
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public EntityStatus Status { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Chilren { get; set; }

        public virtual ICollection<Product> CategoryProducts { get; set; }

        #endregion
    }
}
