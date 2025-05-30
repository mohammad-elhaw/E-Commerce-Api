﻿using Domain.Common;
using Shared;
using Shared.Dtos.ProductModuleDtos;

namespace Services.Contracts
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductResultDto>> GetProductsAsync(ProductSpecificationsParameters parameters);
        public Task<IEnumerable<BrandResultDto>> GetBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetTypesAsync();
        public Task<Result<ProductResultDto>> GetProduct(int id);
        
    }
}
