using System.Collections.Generic;
using System.Threading.Tasks;
using AybCommerce.Domain.Entities;

namespace AybCommerce.Core.Interfaces.Services
{
    public interface ICartItemService 
    { 
        bool UpsertToCartItem(CartItem model, string cartId); 
  
        void RemoveToCartItem(CartItem model, string cartId);   

        List<CartItem> RetrieveCartItems(string cartId);
 
        void ClearCartItems(string cartId); 

        decimal CartTotalAmount(string cartId);
    }
} 
