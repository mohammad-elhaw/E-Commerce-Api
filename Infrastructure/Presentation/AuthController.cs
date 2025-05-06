using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Dtos.IdentityDtos;
using Shared.ErrorModels;
using System.Security.Claims;

namespace Presentation;
public class AuthController(IAuthenticationService authService) : ApiBaseController
{

    [HttpPost("Login")]
    public async Task<ActionResult<UserResponseDto>> Login(LoginDto loginDto)
    {
        var result = await authService.Login(loginDto);
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });

        return Ok(result.Value);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserResponseDto>> Register(RegisterDto registerDto)
    {
        var result = await authService.Register(registerDto);
        
        if(result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var result = await authService.GetCurrentUser(email!);
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }

    [HttpGet("Address")]
    [Authorize]
    public async Task<ActionResult<UserAddressDto>> GetUserAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await authService.GetUserAddress(email!);
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }

    [HttpPut("Address")]
    [Authorize]
    public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UpdateUserAddressDto updateUserAddressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await authService.UpdateUserAddress(email!, updateUserAddressDto);
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }
}
