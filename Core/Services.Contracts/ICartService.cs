using Shared.Dtos.CartModuleDtos;

namespace Services.Contracts
{
    public interface ICartService
    {
        Task<CartDto> CreateOrUpdateCart(CartDto cartDto);
        Task<CartDto?> GetCart(string key);
        Task<bool> DeleteCart(string key);
    }
}
