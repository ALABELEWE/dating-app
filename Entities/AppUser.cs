using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entities;

public class AppUser
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    [MaxLength(50)]
    public required string DisplayName { get; set; }
    [MaxLength(100)]
    public required string Email { get; set; }
    
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}