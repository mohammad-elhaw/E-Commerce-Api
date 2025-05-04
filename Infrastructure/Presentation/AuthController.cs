using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Dtos.IdentityDtos;
using Shared.ErrorModels;

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
}
