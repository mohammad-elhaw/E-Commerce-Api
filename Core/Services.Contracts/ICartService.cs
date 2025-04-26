using Domain.Common;
using Shared.Dtos.CartModuleDtos;

namespace Services.Contracts
{
    public interface ICartService
    {
        Task<CartDto> CreateOrUpdateCart(CartDto cartDto);
        Task<Result<CartDto>> GetCart(string key);
        Task<bool> DeleteCart(string key);
    }
}
