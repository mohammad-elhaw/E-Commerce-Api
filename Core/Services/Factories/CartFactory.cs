using Domain.Entities.CartModule;
using Shared.Dtos.CartModuleDtos;

namespace Services.Factories
{
    public static class CartFactory
    {
        public static Cart ToCart(this CartDto cartDto) =>
            new Cart()
            {
                Id = cartDto.Id,
                Items = MapItemsToCartItem(cartDto.Items),
            };

        public static CartDto ToCartDto(this Cart cart) =>
            new CartDto()
            {
                Id = cart.Id,
                Items = MapItemsToCartItemDto(cart.Items),
            };
        
        private static ICollection<CartItemDto> MapItemsToCartItemDto(ICollection<CartItem> items)
        {
            var cartItems = new List<CartItemDto>();
            foreach (var item in items)
            {
                cartItems.Add(new CartItemDto()
                {
                    Id = item.Id,
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            return cartItems;
        }

        private static List<CartItem> MapItemsToCartItem(ICollection<CartItemDto> items)
        {
            var cartItems = new List<CartItem>();
            foreach(var item in items)
            {
                cartItems.Add(new CartItem()
                {
                    Id = item.Id,
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            return cartItems;
        }

        
    }
}
