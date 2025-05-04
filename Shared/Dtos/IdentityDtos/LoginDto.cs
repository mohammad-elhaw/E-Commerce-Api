using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityDtos;
public class LoginDto
{
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
