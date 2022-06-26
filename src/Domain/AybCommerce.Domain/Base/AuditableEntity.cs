using AybCommerce.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Base 
{    
    public abstract class AuditableEntity : BaseEntity  
    { 
        public virtual DateTime Created { get; set; }

        public virtual DateTime Updated { get; set; } 

        #region Foreign Key(s)

        [Column("CreatedBy")]
        public virtual string CreatedId { get; set; }

        [Column("UpdatedBy")]
        public virtual string UpdatedId { get; set; }

        #endregion

        #region Navigation(s)

        [ForeignKey("CreatedId")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("UpdatedId")]
        public virtual User UpdatedBy { get; set; }

        #endregion
    }
}
