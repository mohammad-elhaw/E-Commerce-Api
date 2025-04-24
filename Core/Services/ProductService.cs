using Domain.Contracts;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Services.Factories;
using Services.Specifications;
using Shared;
using Shared.Dtos;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IConfiguration _configuration) : IProductService
    {
        public async Task<ProductResultDto> GetProductAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            var productToReturn = product.ToProductDto(_configuration["baseUrl"]);
            return productToReturn;
        }

        public async Task<IEnumerable<BrandResultDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(withTrack: false);
            var brandsToReturn = brands.Select(b => b.ToBrandResultDto());
            return brandsToReturn;
        }

        public async Task<IEnumerable<TypeResultDto>> GetTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(withTrack: false);
            var typesToReturn = types.Select(t => t.ToTypeResutDto());
            return typesToReturn;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetProductsAsync(ProductSpecificationsParameters parameters)
        {
            var products = await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var productsToReturn = products.Select(p => p.ToProductDto(_configuration["baseUrl"]));
            
            var theTotalCount = await _unitOfWork.GetRepository<Product, int>()
                .Count(new ProductCountSpecifications(parameters));

            var paginatedResult = new PaginatedResult<ProductResultDto>
            (
                pageIndex: parameters.PageIndex,
                pageSize: parameters.PageSize,
                totalCount: theTotalCount,
                totalPages: (int)Math.Ceiling((double)theTotalCount / parameters.PageSize),
                items: productsToReturn.ToList()
            );

            return paginatedResult;
        }
    }
}
