namespace Shared.Dtos.OrderModuleDto;
public record OrderAddressDto
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}
