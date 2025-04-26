using Domain.Entities.CartModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistance.Repositories
{
    public class CartRepository(IConnectionMultiplexer connection) : ICartRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<Cart?> CreateOrUpdateCart(Cart cart, TimeSpan? timeToLive = null)
        {
            var JsonCart = JsonSerializer.Serialize(cart);
            bool isCreatedOrUpdated = await _database
                .StringSetAsync(cart.Id, JsonCart, timeToLive?? TimeSpan.FromDays(20));
            if(!isCreatedOrUpdated) return null;
            return await GetCart(cart.Id);
        }

        public async Task<bool> DeleteCart(string key) =>
            await _database.KeyDeleteAsync(key);

        public async Task<Cart?> GetCart(string key)
        {
            var cart = await _database.StringGetAsync(key);
            if(cart.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<Cart>(cart!);
        }
    }
}
