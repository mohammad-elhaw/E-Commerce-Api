namespace Shared.Dtos.CartModuleDtos;
public record CartDto
{
    public string Id { get; init; }
    public ICollection<CartItemDto> Items { get; init; }
}

