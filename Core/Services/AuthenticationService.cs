using Domain.Common;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using Shared.Dtos.IdentityDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;
public class AuthenticationService(
    UserManager<User> userManager,
    IConfiguration configuration) : IAuthenticationService
{
    public async Task<Result<UserResponseDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if(user is null) return Result<UserResponseDto>
                .Failure($"User with email {loginDto.Email} not found");

        bool isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid) return Result<UserResponseDto>.Failure("Password is incorrect");
        
        return Result<UserResponseDto>.Success(new UserResponseDto
        {
            Email = user.Email!,
            DisplayName = user.DisplayName,
            Token = await CreateToken(user)
        });

    }

    public async Task<Result<UserResponseDto>> Register(RegisterDto registerDto)
    {
        var user = new User
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            DisplayName = registerDto.DisplayName,
            PhoneNumber = registerDto.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded) 
            return Result<UserResponseDto>
                .Success(new UserResponseDto
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token = await CreateToken(user)
                });

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));

        return Result<UserResponseDto>
            .Failure($"User registration failed: {errors}");
    }


    private async Task<string> CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var roles = await userManager.GetRolesAsync(user);

        foreach(var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var secretKey = Environment.GetEnvironmentVariable("SECRET");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.GetSection("JWTOptions")["Issuer"],
            audience: configuration["JWTOptions:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(20),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
