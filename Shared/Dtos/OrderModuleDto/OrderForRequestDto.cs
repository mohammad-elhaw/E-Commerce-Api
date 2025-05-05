namespace Shared.Dtos.OrderModuleDto;
public record OrderForRequestDto
{
    public string CartId { get; set; } = default!;
    public OrderAddressDto Address { get; set; } = default!;
    public int DeliveryMethodId { get; set; }
}
