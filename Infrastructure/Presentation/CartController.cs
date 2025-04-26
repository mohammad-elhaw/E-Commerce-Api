using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Dtos.CartModuleDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet("{key}")]
        public async Task<ActionResult<CartDto>> GetCart(string key)
        {
            var cart = await cartService.GetCart(key);
            return Ok(cart);
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
}
