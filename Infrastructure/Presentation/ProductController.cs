using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using Shared.Dtos.ProductModuleDtos;

namespace Presentation
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetProducts(
            [FromQuery] ProductSpecificationsParameters parameters)
        {
            var products = await _productService.GetProductsAsync(parameters);
            return Ok(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetTypes()
        {
            var types = await _productService.GetTypesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            return Ok(product);
        }
    }
}
