using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AybCommerce.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public int OrderId { get; set; }

        [StringLength(6)]
        [Column(TypeName = "char(6)")]
        public string BinNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string CardNumber { get; set; }

        public bool IsSuccess => string.IsNullOrEmpty(PaymentLogs.FirstOrDefault(p => p.LogType == PaymentLogType.Response)?.ErrorMessage); // hope it works god :)

        public PaymentStatus PaymentStatus { get; set; }

        #region [ Navigation Properties ]

        [Required]
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }

        #endregion

    }
}