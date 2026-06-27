using System.Security.Cryptography;
using System.Text;
using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers;



public class AccountController(
    ITokenService tokenService, 
    AppDbContext dbContext) : BaseApiController
{


    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registrationDto)
    {
        if (await EmailExists(registrationDto.Email)) return Conflict("Email already exists");
      
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registrationDto.DisplayName,
            Email = registrationDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationDto.Password)),
            PasswordSalt = hmac.Key
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user.toDto(tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await dbContext.Users.
            SingleOrDefaultAsync(u => u.Email == loginDto.Email);
        
        if (user == null) return Unauthorized("Email is invalid");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }
        return user.toDto(tokenService);
    }

    private async Task<bool> EmailExists(string email)
    {
        return await dbContext.Users.AnyAsync(
            u => u.Email == email); 
    }
}