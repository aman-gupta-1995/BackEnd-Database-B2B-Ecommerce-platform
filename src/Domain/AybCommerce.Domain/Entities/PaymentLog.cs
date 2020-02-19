using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class PaymentLog : AuditableEntity
    {
        public PaymentLog()
        {
        }

        public PaymentLog(PaymentLogType logType, string message, string errorMessage = null)
        {
            LogType = logType;
            Message = message;
            ErrorMessage = errorMessage;
        }

        public int? PaymentId { get; set; }

        public PaymentLogType LogType { get; set; }

        public string Message { get; set; } // use for info like request and response

        public string ErrorMessage { get; set; } // use for errors

        #region [ Navigation Properties ]

        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }

        #endregion

    }
}