namespace Shared.Dtos.OrderModuleDto;
public record OrderForResponseDto
{
    public Guid Id { get; init; }
    public required string UserEmail { get; init; }
    public DateTimeOffset OrderDate { get; init; }
    public required string Status { get; init; }
    public required OrderAddressDto Address { get; init; }
    public required string DeliveryMethodName { get; init; }
    public required ICollection<OrderItemDto> items { get; init; }
    public decimal SubTotal { get; init; }
    public decimal Total { get; init; }
}
