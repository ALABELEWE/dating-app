using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Interface;

namespace DatingApp.Extensions;

public static class AppUserExtensions
{
    public static UserDto toDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user)
        };
    }
}