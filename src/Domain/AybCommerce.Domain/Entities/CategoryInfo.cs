using AybCommerce.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class CategoryInfo : AuditableEntity
    {
        public int CategoryId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Key { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string Value { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string Extra { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        #endregion

    }
}
