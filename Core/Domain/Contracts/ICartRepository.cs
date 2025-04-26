using Domain.Entities.CartModule;

namespace Domain.Contracts
{
    public interface ICartRepository
    {
        Task<Cart?> GetCart(string key);
        Task<Cart?> CreateOrUpdateCart(Cart cart, TimeSpan? timeToLive = null);
        Task<bool> DeleteCart(string key);
    }
}
