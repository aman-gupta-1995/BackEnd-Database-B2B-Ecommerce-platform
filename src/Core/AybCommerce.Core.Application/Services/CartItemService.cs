using System.Collections.Generic;
using System.Threading.Tasks;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using System.Linq;
using AybCommerce.Persistance.Data;

namespace AybCommerce.Core.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly AybCommerceDbContext _dbContext;

        public CartItemService(AybCommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private CartItem CartItem(string productCode, string cartId)
        {
            return _dbContext.CartItems.FirstOrDefault(x => x.CartId == cartId && x.ProductCode == productCode);
        }

        private List<CartItem> CartItems(string cartId)
        {
            return _dbContext.CartItems.Where(x => x.CartId == cartId).ToList();
        }

        public bool UpsertToCartItem(CartItem model, string cartId)
        {
            var cartItem = CartItem(model.ProductCode, cartId);

            // Update
            if (cartItem != null)
            {
                cartItem.Quantity = model.Quantity;
                return _dbContext.SaveChanges() > 0;
            }

            // Insert
            var product = _dbContext.Products.FirstOrDefault(x => x.ErpCode == model.ProductCode);
            if (product == null) { return false; }

            model.CartId = cartId;
            model.Price = product.Price;
            model.SalePrice = product.SalePrice;
            model.ProductName = product.Name;
            _dbContext.CartItems.Add(model);
            return _dbContext.SaveChanges() > 0;
        }

        public void RemoveToCartItem(CartItem model, string cartId)
        {
            var cartItem = CartItem(model.ProductCode,cartId);
            _dbContext.CartItems.Remove(cartItem);
            _dbContext.SaveChanges();
        }

        public List<CartItem> RetrieveCartItems(string cartId)
        {
            return CartItems(cartId);
        }

        public void ClearCartItems(string cartId)
        {
            var cartItems = CartItems(cartId);
            _dbContext.CartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public decimal CartTotalAmount(string cartId)
        {
            var cartItems = CartItems(cartId);
            return cartItems.Sum(x => x.SalePrice * x.Quantity);
        }
    }
}
