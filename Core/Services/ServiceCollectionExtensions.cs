using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<ICartService, CartService>();
            return Services;
        }

    }
}
