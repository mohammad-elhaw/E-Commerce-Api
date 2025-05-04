using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Identity;
using Persistance.Repositories;
using StackExchange.Redis;

namespace Persistance;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection Services,
        IConfiguration configuration)
    {
        Services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        });
        Services.AddScoped<IDataSeeding, DataSeeding>();
        Services.AddScoped<IUnitOfWork, UnitOfWork>();
        Services.AddScoped<ICartRepository, CartRepository>();
        Services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
        });
        Services.AddDbContext<AppIdentityDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
        });

        Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        return Services;
    }
}
