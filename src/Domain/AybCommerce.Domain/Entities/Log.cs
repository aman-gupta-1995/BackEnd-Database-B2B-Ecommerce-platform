using AybCommerce.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace AybCommerce.Domain.Entities
{
    public class Log : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Application { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }

        [MaxLength(255)]
        public string Logger { get; set; }

        [MaxLength(5000)]
        public string CallSite { get; set; }

        [MaxLength(5000)]
        public string Exception { get; set; }
    }
}
