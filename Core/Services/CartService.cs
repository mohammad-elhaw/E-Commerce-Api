using Domain.Common;
using Domain.Contracts;
using Services.Contracts;
using Services.Factories;
using Shared.Dtos.CartModuleDtos;

namespace Services
{
    public class CartService(ICartRepository repository) : ICartService
    {
        public async Task<CartDto> CreateOrUpdateCart(CartDto cartDto)
        {
            var cartEntity = cartDto.ToCart();
            var cartReturned = await repository.CreateOrUpdateCart(cartEntity);
            if (cartReturned is null) throw new Exception("Can't Create or Update Cart Not, Try again later.");
            return cartReturned.ToCartDto();
        }

        public async Task<bool> DeleteCart(string key) =>
            await repository.DeleteCart(key);
        

        public async Task<Result<CartDto>> GetCart(string key)
        {
            var cartEntity = await repository.GetCart(key);
            if (cartEntity is not null) 
                return Result<CartDto>.Success(cartEntity.ToCartDto());
            return Result<CartDto>.Failure($"Cart with id: {key} not found");
        }
    }
}
