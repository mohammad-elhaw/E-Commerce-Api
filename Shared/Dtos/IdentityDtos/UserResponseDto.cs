namespace Shared.Dtos.IdentityDtos;
public class UserResponseDto
{
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
