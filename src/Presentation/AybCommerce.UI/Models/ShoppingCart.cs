using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AybCommerce.UI.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }

        private ShoppingCart()
        {

        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart() { Id = cartId };
        }

        // after completed order
        public static void RemoveCartSession(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            session.SetString("CartId", string.Empty);
        }
    }
}
