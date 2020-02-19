using System;
using AybCommerce.Domain.Base;
using AybCommerce.Domain.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    [Obsolete("use cart from session")]
    // todo may remove - move session bps
    public class Cart : AuditableEntity
    {
        public Cart()
        {

        }

        public Cart(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public CartStatus Status { get; set; } = CartStatus.Active;

        #region [ Navigation Properties ]

        public virtual ICollection<CartItem> CartItems { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion

    }
}
