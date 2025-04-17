using Domain.Contracts;
using System.Text.Json;

namespace Persistance.Data.DataSeeding
{
    public class DbInitializer(AppDbContext _dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                    if (!_dbContext.ProductTypes.Any())
                    {
                        var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\types.json");
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if(types is not null && types.Any())
                        {
                            await _dbContext.AddRangeAsync(types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (!_dbContext.ProductBrands.Any())
                    {
                        var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\brands.json");
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        if(brands is not null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (!_dbContext.Products.Any())
                    {
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if(products is not null && products.Any())
                        {
                            await _dbContext.AddRangeAsync(products);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
