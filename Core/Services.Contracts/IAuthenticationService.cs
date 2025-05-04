using Domain.Common;
using Shared.Dtos.IdentityDtos;

namespace Services.Contracts;
public interface IAuthenticationService
{
    Task<Result<UserResponseDto>> Login(LoginDto loginDto);
    Task<Result<UserResponseDto>> Register(RegisterDto registerDto);

}
