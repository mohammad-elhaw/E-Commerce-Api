using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Dtos.CartModuleDtos;
using Shared.ErrorModels;

namespace Presentation;
[Authorize]
public class CartController(ICartService cartService) : ApiBaseController
{
    [HttpGet("{key}")]
    public async Task<ActionResult<CartDto>> GetCart(string key)
    {
        var result = await cartService.GetCart(key);
        if(result.IsFailure)
            return NotFound(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status404NotFound
            });
        return Ok(result.Value);
    }
    [HttpPost]
    public async Task<ActionResult<CartDto>> CreateOrUpdateCart([FromBody] CartDto cartDto)
    {
        var cart = await cartService.CreateOrUpdateCart(cartDto);
        return CreatedAtAction(nameof(GetCart), new { key = cart.Id }, cart);
    }
    [HttpDelete("{key}")]
    public async Task<ActionResult> DeleteCart(string key)
    {
        var isDeleted = await cartService.DeleteCart(key);
        if (isDeleted) return NoContent();
        return NotFound();
    }
}
