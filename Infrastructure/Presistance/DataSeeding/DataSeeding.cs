using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
using Persistance.Data;
using System.Text.Json;

namespace Persistance;

public class DataSeeding(
    AppDbContext dbContext,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IDataSeeding
{
    public async Task DataSeed()
    {
        try
        {
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                await dbContext.Database.MigrateAsync();

            if (!await dbContext.ProductTypes.AnyAsync())
            {
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\DataSeeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Any())
                    await dbContext.AddRangeAsync(types);
            }

            if (!await dbContext.ProductBrands.AnyAsync())
            {
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\DataSeeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands is not null && brands.Any())
                    await dbContext.AddRangeAsync(brands);
            }

            if (!await dbContext.Products.AnyAsync())
            {
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Any())
                    await dbContext.AddRangeAsync(products);
            }
        
            if(!await dbContext.Set<DeliveryMethod>().AnyAsync())
            {
                var dmData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\DataSeeding\delivery.json");
                var dm = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                if (dm is not null && dm.Any())
                    await dbContext.AddRangeAsync(dm);
            }

            if (dbContext.ChangeTracker.HasChanges())
                await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle Exception
        }
    }

    public async Task IdentityDataSeed()
    {
        try
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!await userManager.Users.AnyAsync())
            {
                var user1 = new User
                {
                    Email = "mohammadelhaw@gmail.com",
                    UserName = "mohammadelhaw",
                    DisplayName = "Mohammed Elhaw",
                    PhoneNumber = "01000000000",
                };

                var user2 = new User
                {
                    Email = "salmamohammad@gmail.com",
                    UserName = "salmamohammad",
                    DisplayName = "Salma Mohammed",
                    PhoneNumber = "01000000000",
                };
                await userManager.CreateAsync(user1, "P@ssw0rd");
                await userManager.CreateAsync(user2, "P@ssw0rd");

                await userManager.AddToRoleAsync(user1, "SuperAdmin");
                await userManager.AddToRoleAsync(user2, "Admin");
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }

    
    
    }
}
