using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.CartModuleDtos
{
    public record CartItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } 
        public string PictureUrl { get; init; }
        [Range(1, 100)]
        public int Quantity { get; init; }
        [Range(1, Double.MaxValue)]
        public decimal Price { get; init; }
    }
}