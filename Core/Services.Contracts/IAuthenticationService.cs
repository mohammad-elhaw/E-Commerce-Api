using Domain.Common;
using Shared.Dtos.IdentityDtos;

namespace Services.Contracts;
public interface IAuthenticationService
{
    Task<Result<UserResponseDto>> Login(LoginDto loginDto);
    Task<Result<UserResponseDto>> Register(RegisterDto registerDto);
    Task<Result<UserResponseDto>> GetCurrentUser(string email);
    Task<bool> IsEmailExists(string email);
    Task<Result<UserAddressDto>> GetUserAddress(string email);
    Task<Result<UserAddressDto>> UpdateUserAddress(string email, UpdateUserAddressDto updateUserAddressDto);
}
