using Domain.Entities.ProductModule;
using Shared.Dtos.ProductModuleDtos;

namespace Services.Factories
{
    public static class ProductFactory
    {
        public static ProductResultDto ToProductDto(this Product product, string baseImageUrl) =>
            new ProductResultDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = $"{baseImageUrl}{product.PictureUrl}",
                Price = product.Price,
                BrandName = product.ProductBrand?.Name,
                TypeName = product.ProductType?.Name,
            };

        public static BrandResultDto ToBrandResultDto(this ProductBrand brand) =>
            new BrandResultDto()
            {
                Id = brand.Id,
                Name = brand.Name
            };

        public static TypeResultDto ToTypeResutDto(this ProductType productType) =>
            new TypeResultDto()
            {
                Id = productType.Id,
                Name = productType.Name,
            };
    }
}
