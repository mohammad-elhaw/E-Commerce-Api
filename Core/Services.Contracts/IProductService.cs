using Shared;
using Shared.Dtos;

namespace Services.Contracts
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductResultDto>> GetProductsAsync(ProductSpecificationsParameters parameters);
        public Task<IEnumerable<BrandResultDto>> GetBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetTypesAsync();
        public Task<ProductResultDto> GetProductAsync(int id);
        
    }
}
