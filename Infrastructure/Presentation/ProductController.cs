using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using Shared.Dtos.ProductModuleDtos;
using Shared.ErrorModels;

namespace Presentation;
public class ProductController(IProductService _productService) : ApiBaseController
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
        var result = await _productService.GetProduct(id);
        if(result.IsFailure)
            return NotFound(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status404NotFound
            });
        
        return Ok(result.Value);
    }
}
