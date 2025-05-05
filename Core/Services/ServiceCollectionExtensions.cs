using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.Text;

namespace Services;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection Services,
        IConfiguration configuration)
    {
        Services.AddScoped<IProductService, ProductService>();
        Services.AddScoped<ICartService, CartService>();
        Services.AddScoped<IAuthenticationService, AuthenticationService>();
        Services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = configuration["JWTOptions:Issuer"],
                ValidAudience = configuration["JWTOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))
                    ),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            };
        });
        Services.AddScoped<IOrderService, OrderService>();
        return Services;
    }

}
